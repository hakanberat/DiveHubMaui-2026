import fs from 'node:fs/promises';
import path from 'node:path';
import { fileURLToPath } from 'node:url';
const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const read = p => fs.readFile(path.join(root,p),'utf8');
const write = (p,s) => fs.writeFile(path.join(root,p),s,'utf8');
const decode = s => s.replace(/&#(x[\da-f]+|\d+);/gi,(_,x)=>String.fromCodePoint(x[0].toLowerCase()==='x'?parseInt(x.slice(1),16):+x)).replace(/&nbsp;/g,' ').replace(/&amp;/g,'&').replace(/&quot;/g,'"').replace(/&apos;|&#039;/g,"'").replace(/&ndash;/g,'–').replace(/&rsquo;/g,'’');
const clean = s => decode(s.replace(/<[^>]+>/g,' ')).replace(/\s+/g,' ').trim();
const article = async name => {let s=await read('.reference/'+name+'.html'); let a=s.indexOf('<div id="theme-page"'); let b=s.indexOf('<section id="mk-footer'); if(a<0)throw Error(name); return s.slice(a,b>a?b:undefined);};
const paragraphs = s => {let arr=[];for(const m of s.matchAll(/<(h[1-6]|p|li)\b[^>]*>([\s\S]*?)<\/\1>/g)){let t=clean(m[2]);if(/^Gal[l]?er?y$/i.test(t))break;if(t&&!/^We.re not around|^List of courses/.test(t))arr.push(t);}return arr;};
const entry=(Id,Kind,Category,Name,Image,Summary,Description,Source)=>({Id,Kind,Category,Name,Image,Summary,Description,Source,SourceNote:''});
const entries=[],assets=[],gallery=[],team=[],downloads=[];
const siteData=[
['kocareis','Kocareis Beach',6,'Beginners / photography','Unavailable','ocean_placeholder.png','Sunlit shallows on the east coast.','Kocareis Beach'],
['balloon','Balloon',13,'Beginners','Unavailable','ocean_placeholder.png','A sunken water carrier near Bogaz.','Balloon'],
['bogaz','Bogaz Ship Wreck',5,'Novice divers','Unavailable','bogazshipwreck.png','Two pieces of a torpedo boat in shallow water.','Bogaz Ship Wreck'],
['alp','Alp',13,'PADI Open Water / CMAS 1* or equivalent','Boat','alp.png','Seagrass, sand rays and a gentle rocky wall.','Alp'],
['bedri','Bedri',18,'PADI Open Water / CMAS 1* or equivalent','Boat','bedri.png','Neptune grass, corals and ancient artefacts.','Bedri'],
['pitos','Pitos',32,'PADI Advanced Open Water / CMAS 2** or equivalent','Boat','pitos.png','A rocky plateau with caverns and ancient anchors.','Pitos'],
['noahsark',"Noah's Ark",27,'PADI Advanced Open Water / CMAS 2** or equivalent','Boat','noahsark.png','Schools of fish, sponges and amphora along a wall.','Noah’s Ark'],
['palm','Palm Beach',12,'Beginners / photography','Unavailable','ocean_placeholder.png','Shallow-water photography off Famagusta.','Palm Beach'],
['raycave','Ray Cave',28,'PADI Advanced Open Water / CMAS 2** or equivalent','Boat','raycave.png','Sand rays and curious schools of black bream.','Ray Cave'],
['tomofil','Tomofil Car Wreck',12,'Beginners','Unavailable','ocean_placeholder.png','A vintage VW Beetle beneath the Bogaz waters.','Tomofil'],
['abdulselam','Abdulselam',32,'CMAS 2** or equivalent','Boat','abdulselam.png','A compact site of sponges, algae and sea stars.','Abdulselam'],
['awall','A-Wall / Amphora Wall',38,'PADI Advanced Open Water / CMAS 2** or equivalent','Boat','amphorawall.png','Ancient anchors and amphora on a Mediterranean wall.','A-Wall']];
const sitesHtml=await article('website_services_dive-sites');
const heads=[...sitesHtml.matchAll(/<h2\b[^>]*>[\s\S]*?<\/h2>/g)];
for(const s of siteData){
 let i=heads.findIndex(m=>clean(m[0])===s[7]);if(i<0)throw Error(s[1]);
 let body=paragraphs(sitesHtml.slice(heads[i].index+heads[i][0].length,heads[i+1]?.index)).join('\n\n');
 let e=entry(s[0],'site','Northern Cyprus',s[1],s[5],s[6],body,'https://dive-hub.com/en/services/dive-sites/');
 Object.assign(e,{MaxDepth:s[2],Experience:s[3],Access:s[4]});
 if(['pitos','awall'].includes(s[0]))e.SourceNote='The seabed depth shown here differs from the historical operational limit of 30 m stated in the archive.';
 if(s[5]==='ocean_placeholder.png')e.SourceNote='A site-specific photograph and access type were not verified. The illustration is a local placeholder.';
 entries.push(e);
}
const legacy='.reference/legacy/DiveHub-Android-Legacy-main';
for(const s of [['shore','Dive Hub Shore',8,'Shore','shore.png','divehubshore'],['amphorabeach','Amphora Beach',15,'Boat','amphorabeach.png','amphorabeach']]){
 let body=(await read('.reference/text/legacy_'+s[5]+'.txt')).replace(/^\uFEFF/,'').trim();
 let e=entry(s[0],'site','Legacy collection',s[1],s[4],body.split('. ')[0]+'.',body,'https://github.com/hakanberat/DiveHub-Android-Legacy/blob/main/res/layout/'+s[5]+'.xml');
 Object.assign(e,{MaxDepth:s[2],Access:s[3],Experience:'Beginners',SourceNote:'Preserved from 2014. The website does not list this name; it has not been assumed to be another site.'}); entries.push(e);
}
const photoNames=['pic1','pic2','pic3','pic4','pic5','pic7','pic8','pic10','pic11','pic12','pic15','pic16','am2','d1','d3','d4','d7','d9','wr1','wr2','ray2','cr1','ani8','ani9','ani12','ani16'];
const coverNames=['shore','amphorabeach','bogazshipwreck','alp','bedri','pitos','noahsark','raycave','abdulselam','amphorawall'];
for(const name of new Set([...photoNames,...coverNames])){
 await fs.copyFile(path.join(root,legacy,'res/drawable/'+name+'.png'),path.join(root,'Resources/Images/'+name+'.png'));
 assets.push({File:name+'.png',Source:'https://github.com/hakanberat/DiveHub-Android-Legacy/blob/main/res/drawable/'+name+'.png',Type:'Legacy original'});
}
for(const name of photoNames)gallery.push({Id:name,Image:name+'.png',Caption:'Dive Hub archive · '+name,Source:'2014 Android gallery. Original caption unavailable.'});
const allFiles=await fs.readdir(path.join(root,'.reference'));
const courseFiles=allFiles.filter(s=>/^website_courses_(padi|cmas|dan)-courses_.+\.html$/.test(s)&&!/_specialities.html$/.test(s)).concat(allFiles.filter(s=>/^website_tdi_/.test(s)&&!/(cart|feed|galeries|news)\.html$/.test(s)));
for(const file of courseFiles){
 const tdi=file.startsWith('website_tdi_');
 const cat=tdi?'TDI':file.match(/^website_courses_([a-z]+)-/)[1].toUpperCase();
 const slug=file.replace(/^website_(tdi_|courses_[a-z]+-courses_)/,'').replace(/\.html$/,'');
 const lines=paragraphs(await article(file.replace(/\.html$/,'')));if(lines.length<2)throw Error(file);
 let name=lines[0].toLowerCase().replace(/\b[a-z]/g,c=>c.toUpperCase()).replace(/^Cmas/,'CMAS');
 let body=lines.slice(1).join('\n\n').replace(/€\s*[\d,.]+/g,'').replace(/All is included in the price\./g,'').trim();
 let summary=lines[1].split(/(?<=[.!?])\s+/)[0];if(summary.length>170)summary=summary.slice(0,167)+'…';
 const url=tdi?'https://dive-hub.com/en/'+slug+'/':'https://dive-hub.com/en/courses/'+cat.toLowerCase()+'-courses/'+slug+'/';
 let e=entry(cat.toLowerCase()+'_'+slug,'course',cat,name,cat==='TDI'?'d3.png':cat==='DAN'?'d7.png':'pic3.png',summary,body,url);
 Object.assign(e,{MinimumAge:body.match(/Min Age\s*\(([^)]+)\)/)?.[1]+' years (archived)',Prerequisites:body.match(/Min Certification\s*\(([^)]+)\)/)?.[1]||'Unavailable',DiveCount:body.match(/Dives\s*\(([^)]+)\)/)?.[1]||'Unavailable'});
 if(e.MinimumAge.startsWith('undefined'))e.MinimumAge='Unavailable';
 let age=body.match(/[Mm]inimum age (?:is )?([^\n.]+)/)?.[1];if(age)e.MinimumAge=age+' (archived)';
 if(cat==='CMAS')e.Prerequisites=body.match(/Requirements: ([^\n]+)/)?.[1]||e.Prerequisites;
 if(cat==='TDI')e.Prerequisites=lines.filter(s=>/^Minimum certification|^Have a minimum|^Provide proof|^Show proof|^Certified as/.test(s)).join('; ')||'Unavailable';
 e.SourceNote='Historical course description, not current availability or certification standards. Archived ages and depth limits require confirmation with the training agency. Prices are omitted. Dive Hub photograph, illustrative.';
 entries.push(e);
}
const special=await article('website_courses_padi-courses_specialities');
const sh=[...special.matchAll(/<h2\b[^>]*>[\s\S]*?<\/h2>/g)];
for(let i=1;i<sh.length;i++){
 let name=clean(sh[i][0]).toLowerCase().replace(/\b[a-z]/g,c=>c.toUpperCase());if(!name)continue;
 let body=paragraphs(special.slice(sh[i].index+sh[i][0].length,sh[i+1]?.index)).join('\n\n');if(!body)continue;
 let e=entry('padi_special_'+i,'course','PADI',name,'pic3.png',body.split(/(?<=[.!?])\s+/)[0],body,'https://dive-hub.com/en/courses/padi-courses/specialities/');
 Object.assign(e,{MinimumAge:(body.match(/Min Age\s*\(([^)]+)\)/)?.[1]||'Unavailable')+' (archived)',Prerequisites:body.match(/Min Certification\s*\(([^)]+)\)/)?.[1]||'Unavailable',DiveCount:body.match(/Dives\s*\(([^)]+)\)/)?.[1]||'Unavailable',SourceNote:'Archived training information, not current certification standards. Dive Hub photograph, illustrative.'});entries.push(e);
}
for(const s of [['try-dives','Try Dives','pic2.png','A first experience beneath the Mediterranean.'],['shore-dives','Shore Dives','shore.png','Shallow-water exploration from the shore.'],['boat-dives','Boat Dives','d1.png','Explore the coast from the water.'],['night-dives','Night Dives','ani16.png','A different view of familiar habitats.'],['dives','Guided Dives','pic1.png','Dive experiences from the Dive Hub archive.']]){
 let lines=paragraphs(await article('website_services_'+s[0])).slice(1);let end=lines.findIndex(s=>['Boat Dives','Night Dives','Shore Dives','Try Dives'].includes(s));
 let e=entry(s[0],'service','Dive experiences',s[1],s[2],s[3],lines.slice(0,end<0?undefined:end).join('\n\n'),'https://dive-hub.com/en/services/'+s[0]+'/');
 e.SourceNote='Historical service description. Operating status, schedules and availability are not verified.';entries.push(e);
}
let i=0;
for(const m of (await article('website_about_our-team')).matchAll(/<li class="mk-employee-item[\s\S]*?<\/li>/g)){
 let Name=clean(m[0].match(/class="team-member-name[^"]*">([\s\S]*?)<\/span>/)?.[1]||'');
 let Role=clean(m[0].match(/class="team-member-position[^"]*">([\s\S]*?)<\/span>/)?.[1]||'');
 let Source=m[0].match(/"default":"([^"]+)"/)?.[1];if(!Name||!Source)continue;
 let Image='team_'+(++i)+'.jpg';team.push({Name,Role,Image});downloads.push({File:Image,Source,Type:'Website team portrait'});
}
downloads.push({File:'divehub_logo.png',Source:'https://dive-hub.com/wp-content/uploads/2015/07/logo2-01.png',Type:'Official logo'});
for(const [File,url] of [['ocean_hero.jpg','2017/06/dsc_0034-1024x768.jpg'],['ocean_school.jpg','2017/06/dsc_0049-1024x768.jpg'],['ocean_reef.jpg','2017/06/dsc_1310-1024x768.jpg'],['ocean_diver.jpg','2015/08/dsc_00w06-1024x768.jpg'],['ocean_water.jpg','2015/08/dsc_1450-1024x768.jpg'],['center_panorama.jpg','2019/05/pano11-3-1024x362.jpg']]){
 downloads.push({File,Source:'https://dive-hub.com/wp-content/uploads/'+url,Type:'Website photograph'});
 gallery.push({Id:File.replace(/\.jpg$/,''),Image:File,Caption:'Dive Hub website archive',Source:'Original file: '+url+'. Caption unavailable.'});
}
const About="Dive Hub was a scuba diving centre on the coast of Iskele Bogaz in Northern Cyprus. Its published story brings together shallow Mediterranean shores, offshore walls, marine life and the ancient artefacts left along Cyprus's sea trade routes.\n\nThe centre described a relaxed, social atmosphere, with individual and group training alongside guided diving. This app preserves that history and the photography and information published by Dive Hub.\n\nThe original Android app was developed in 2014. This offline modernization combines its collection with accessible website material. It does not establish whether the centre currently operates or accepts bookings.";
const Contacts=[{Label:'Historical head office',Value:'Kordon Boyu Str. No:A10, Bahceler / Iskele, Northern Cyprus'},{Label:'Historical office telephone',Value:'+90 392 371 3838'},{Label:'Historical mobile telephone',Value:'+90 542 860 0119'},{Label:'Original website',Value:'https://dive-hub.com/en/'}];
for(const e of entries){
 if(e.Id==='dan_bsaic-life-support-bls')e.Name='Basic Life Support (BLS)';
 if(e.Id==='tdi_advanced-nitrox-introduction-to-tech-diving'){e.Description='The archived website lists this combined course and four training dives. Further course-specific details are unavailable.';e.Summary='A combined course listed in the historical TDI catalog.';e.DiveCount='4';}
 if(e.Id==='tdi_intro-to-tech-advanced-nitrox-deco-procedures'){e.Description='The archived website lists this combined course. A substantive description, minimum age and prerequisites are unavailable on its detail page.';e.Summary='A combined course listed in the historical TDI catalog.';}
 if(e.Kind==='course')e.Image=e.Category==='TDI'?'ocean_school.jpg':e.Category==='DAN'?'ocean_water.jpg':'ocean_reef.jpg';
}
function normalize(e){
 if(e.Kind!=='course')return;
 if(e.MinimumAge?.startsWith('N/A'))e.MinimumAge='Unavailable';
 if(e.Prerequisites==='N/A')e.Prerequisites='No prior certification listed (archived)';
 if(e.Prerequisites==='OW')e.Prerequisites='Open Water certification (archived)';
 if(e.Prerequisites==='AOW')e.Prerequisites='Advanced Open Water certification (archived)';
 if(e.Id==='padi_kids-bubblemaker')e.MinimumAge='8 years (archived)';
 if(e.Id==='cmas_cmas-1')e.Prerequisites='Swimming ability, a health declaration and at least a primary school qualification. Parental approval was listed for divers under 18.';
 const count=e.Description.match(/(?:^|\n)(\d+) dives/i)?.[1];
 if(count&&e.DiveCount==='Unavailable')e.DiveCount=count;
 if(e.Id==='tdi_nitrox-diver')e.DiveCount='0 required in the archive';
 if(e.Id==='tdi_intro-to-tech')e.DiveCount='3';
 if(e.Id==='tdi_advanced-nitrox-diver')e.DiveCount='4';
 if(e.Id==='tdi_advanced-nitrox-decompression-procedures-2')e.MinimumAge='18 years (archived)';
 e.Description=e.Description.replace(/,\s*(?=\n|$)/g,'');
}
entries.forEach(normalize);
const cleanCatalogCopy = function(data) {
 const cleanValue=value=>{
  if(typeof value!=='string')return value;
  const text=value.replace(/\s*\((?:archive|archived)\)/gi,'').trim();
  return /^(?:unavailable|unavalibale|n\/a)$/i.test(text)?'':text;
 };
 for(const entry of data.Entries){
  for(const key of ['Access','Experience','MinimumAge','Prerequisites','DiveCount'])if(key in entry)entry[key]=cleanValue(entry[key]);
  if(entry.Category==='Legacy collection')entry.Category='Northern Cyprus';
  entry.SourceNote=['pitos','awall'].includes(entry.Id)
   ?'Site depth: '+entry.MaxDepth+' m. Dive depth limit described for this site: 30 m.'
   :'';
  if(entry.Id==='tdi_advanced-nitrox-introduction-to-tech-diving'){
   entry.Summary='Advanced Nitrox and Introduction to Tech Diving combined.';
   entry.Description='Combined Advanced Nitrox and Introduction to Tech Diving course with four training dives.';
  }
  if(entry.Id==='tdi_intro-to-tech-advanced-nitrox-deco-procedures'){
   entry.Summary='Intro to Tech, Advanced Nitrox and Decompression Procedures combined.';
   entry.Description='';
  }
  if(entry.Id==='tdi_nitrox-diver')entry.DiveCount='0';
  if(entry.Id==='dives')entry.Summary='Explore marine life and underwater landscapes along the coast of Northern Cyprus.';
 }
 data.Gallery.forEach((photo,index)=>{
  photo.Caption='Dive Hub · Photo '+String(index+1).padStart(2,'0');
  const original=photo.Source.match(/^Original file: (.+?)\. Caption/);
  if(original)photo.Source='https://dive-hub.com/wp-content/uploads/'+original[1];
  else if(!photo.Source.startsWith('https://'))photo.Source='https://github.com/hakanberat/DiveHub-Android-Legacy/blob/main/res/drawable/'+photo.Image;
 });
 data.About="Dive Hub's story is rooted in Iskele Bogaz on the eastern coast of Northern Cyprus. Mediterranean shores, offshore walls, marine life and ancient artefacts define this part of the island.\n\nDive Hub brought together guided diving and individual or group training in a relaxed, social setting by the sea.";
 for(const contact of data.Contacts){
  contact.Label=contact.Label.replace(/^Historical /i,'').replace(/^Original website$/,'Website');
  contact.Label=contact.Label.charAt(0).toUpperCase()+contact.Label.slice(1);
 }
 return data;
};
await write('Resources/Raw/catalog.json',JSON.stringify(cleanCatalogCopy({Entries:entries,Gallery:gallery,Team:team,About:About+"\n\nThe 2014 team list also named K. M. Hakan Koçdağ; the newer website names Hakan Berat Dermican instead. They have not been assumed to be the same person.",Contacts}),null,2));
await write('docs/asset-manifest.json',JSON.stringify([...assets,...downloads],null,2));
await write('.reference/images.curl',downloads.flatMap(d=>['url = "'+d.Source+'"','output = "Resources/Images/'+d.File+'"']).join('\n'));
console.log(JSON.stringify({entries:entries.length,gallery:gallery.length,team:team.length,assets:assets.length+downloads.length}));
