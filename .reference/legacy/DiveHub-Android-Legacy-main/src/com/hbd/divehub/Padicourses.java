package com.hbd.divehub;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnChildClickListener;
import android.widget.ExpandableListView.OnGroupClickListener;
import android.widget.ExpandableListView.OnGroupCollapseListener;
import android.widget.ExpandableListView.OnGroupExpandListener;

public class Padicourses extends Activity{
	
	ExpandableListAdapter listAdapter;
	ExpandableListView expListView;
	List<String> listDataHeader;
	HashMap<String, List<String>> listDataChild;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.padicourses);
		
		// get the listview
		expListView = (ExpandableListView) findViewById(R.id.lvExp);

		// preparing list data
		prepareListData();

		listAdapter = new ExpandableListAdapter(this, listDataHeader, listDataChild);

		// setting list adapter
		expListView.setAdapter(listAdapter);

		// Listview Group click listener
		expListView.setOnGroupClickListener(new OnGroupClickListener() {

			@Override
			public boolean onGroupClick(ExpandableListView parent, View v,
					int groupPosition, long id) {
				// Toast.makeText(getApplicationContext(),
				// "Group Clicked " + listDataHeader.get(groupPosition),
				// Toast.LENGTH_SHORT).show();
				return false;
			}
		});

		// Listview Group expanded listener
		expListView.setOnGroupExpandListener(new OnGroupExpandListener() {

			@Override
			public void onGroupExpand(int groupPosition) {
				/*Toast.makeText(getApplicationContext(),
						listDataHeader.get(groupPosition) + " Expanded",
						Toast.LENGTH_SHORT).show();*/
			}
		});

		// Listview Group collasped listener
		expListView.setOnGroupCollapseListener(new OnGroupCollapseListener() {

			@Override
			public void onGroupCollapse(int groupPosition) {
				/*Toast.makeText(getApplicationContext(),
						listDataHeader.get(groupPosition) + " Collapsed",
						Toast.LENGTH_SHORT).show();*/

			}
		});

		// Listview on child click listener
		expListView.setOnChildClickListener(new OnChildClickListener() {

			@Override
			public boolean onChildClick(ExpandableListView parent, View v,
					int groupPosition, int childPosition, long id) {
				// TODO Auto-generated method stub
				/*Toast.makeText(
						getApplicationContext(),
						listDataHeader.get(groupPosition)
								+ " : "
								+ listDataChild.get(
										listDataHeader.get(groupPosition)).get(
										childPosition), Toast.LENGTH_SHORT)
						.show();*/
				return false;
			}
		});
}
	private void prepareListData() {
		listDataHeader = new ArrayList<String>();
		listDataChild = new HashMap<String, List<String>>();

		// Adding child data
		listDataHeader.add("KIDS (Bubblemaker)");
		listDataHeader.add("Discover Scuba Diving");
		listDataHeader.add("Scuba Diver");
		listDataHeader.add("Open Water Diver");
		listDataHeader.add("Open Water Referral");
		listDataHeader.add("Upgrade to Open Water");
		listDataHeader.add("SPECIALTIES");
		listDataHeader.add("Advanced Open Water Diver");
		listDataHeader.add("Adventure Diver");
		listDataHeader.add("Emergency First Response");
		listDataHeader.add("Rescue Diver");
		listDataHeader.add("Dive Master");
		


		// Adding child data
		List<String> kids  = new ArrayList<String>();
		kids.add("Bubblemaker programme is for 8 year olds and above giving them the chance to experience diving in confined water with full scuba gear. Our qualified and friendly instructors teach in a safe, confined environment and help all young divers 'learn through play' to enjoy their experience and acquire their first scuba skills. A PADI bubble maker certificate is presented to all participants.");
		
		List<String> dsd  = new ArrayList<String>();
		dsd.add("You are a little nervous about the first time you breathe under the water? PADI Discover Scuba Diving, typically the first step becoming a diver, is probably right for you. On your arrival to Dive Hub, you will always find a welcoming instructor to go through a short theory session with you before your underwater practice. You will go through all the information briefly, enough to understand about what’s happening under the water and know what to do. The rest is just fun! Accompanied by Padi professional, you will go through your underwater sessions in which you will learn to do some specific skills. After the briefing and the underwater skills practice, you are more confident and are ready to see the vast underwater life of iskele Dive Hub shore, where you can meet the crabs, squirrel fish schools, curious pope fish schools even a sea horse. Enjoy the underwater! ");
		
		List<String> sb  = new ArrayList<String>();
		sb.add("Short on time? Or want to take one step at a time? Open Water Diver Course seems too much and too long for you right now? Become a Scuba Diver. With this course, you learn the first three sections of Open Water Course and practice the first two dives in the sea. After becoming a Scuba Diver, you can dive down to 12 metres under the supervision of a dive professional and explore the underwater life and creatures as they welcome you. After certifying as a Scuba Diver, you can upgrade your level to Open Water at any time.Min Certification (N/A) Min Age (10) Dives (2) ");
		
		List<String> owd  = new ArrayList<String>();
		owd.add("Explore what’s beneath, independently with a dive buddy get your first full qualification! Qualifying as an Open Water Diver allows you to dive independently to a max of 18 metres with a dive buddy. The course consist of 5 theory modules, 5 pool/confined water sessions and 4 open water dives.Please remember, the qualification does not expire but it is recommended to take a refresher dive if you have not been diving for 6 months or longer.All is included in the price.Min Certification (N/A) Min Age (10) Dives(4) ");
		
		List<String> owf  = new ArrayList<String>();
		owf.add("You do not like the idea of diving in cold boring waters in your homeland? You prefer to dive in the Mediterranean but do not want to spend your precious time learning the theory of diving and doing the confined water sessions during your holiday? Instead you would like go in the warm sea straight away. You can come and complete the 4 training dives in the Open Water with us in the warm waters of Cyprus. Your Open Water Qualification will be issued from our dive centre at the end of training.Min Certification (Referral Form) Min Age (10) Dives (4) ");
		
		List<String> uof  = new ArrayList<String>();
		uof.add(" This is the second step for Scuba Divers. In this course, you will cover the 2 extra sessions of theory and pool training in addition to the last two dives in the open water, to become qualified as an Open Water Diver.Min Certification (Scuba Diver) Min Age (10) Dives (2)");
		
		List<String> aowd  = new ArrayList<String>();
		aowd.add("Why stay in the beginners’ depth limit while there is so much to experience and explore? Advance your scuba skills and extend your depth limit to 30 meters with Advanced Open Water Course. Chose 5 dives in the areas of your interest for gaining more experience and get qualified as Advanced Open Diver for more freedom under the water. During the course, you will complete two core dives which are underwater navigation and deep dive, and three elective dives according to your area of interest.Min Certification (OW) Min Age (10) Dives (5)  ");
		
		List<String> ad  = new ArrayList<String>();
		ad.add("Extend your Dive Skills! Take part to Adventure Dives and see what more can you do underwater. We offer you a wide selection of Adventure dives (at least two different each day) to choose from. These dives will also count for your Advanced Diver Certification and the relative specialty certification.Min Certification (OW) Min Age (10) Dives (3) ");
		
		List<String> efr  = new ArrayList<String>();
		efr.add(" A step on the way to becoming a Rescue Diver, this first aid course will teach you all you need to know to help in any kind of accident. The course is not specific to diving and therefore; teaches things that everybody really should learn as you never know when you are going to need to help somebody. The Emergency First Response Primary Care (CPR in case of life threatening emergencies) and Emergency First Response Secondary Care (First Aid in case of injuries and illnesses that are not immediately life threatening) and the Emergency First Response Care For Children courses teach you how to provide emergency care in various situations. These courses are structured in such a way that learning is made easy by providing a non stressful environment in which you can practice and apply emergency care skills through realistic scenario practice.Min Certification (N/A) Min Age (N/A) Dives (0)");
		
		List<String> rd  = new ArrayList<String>();
		rd.add("The Rescue Diver course is a crucial phase of your training where you start looking beyond yourself and start thinking about welfare and care of others. In fact, the course helps develop your ability to identify and prevent problems before they happen, as much as solve them if they arise. The course is practically based; structured and organised to help learning and to create the proper mindset, in the diver, to deal with different kinds of emergencies.Min Certification (AOW) Min Age (10) Dives (4) ");
		

		List<String> dm = new ArrayList<String>();
		dm.add("This is the first professional step on the PADI ladder. It can be difficult, challenging but is ultimately incredibly rewarding and you really will feel like a professional diver when you are finished. During the PADI Divemaster course you will learn about supervising certified divers, assisting with students, risk management, the physiology of diving, dive equipment, the physics of diving, decompression theory, the recreational dive planner, diving skills, the underwater environment and how to independently conduct certain PADI programs and courses.You will be required to prove your water skills and stamina and be taught how to demonstrate skills for students and continue to hone your rescue techniques. You will gain practical experience by completing an emergency assistance plan and underwater map and either by assisting on courses or by a series of scenarios.");
		
		List<String> sp = new ArrayList<String>();
		sp.add("Please call for more information.");

		
		
		

		listDataChild.put(listDataHeader.get(0), kids); // Header, Child data
		listDataChild.put(listDataHeader.get(1), dsd);
		listDataChild.put(listDataHeader.get(2), sb);
		listDataChild.put(listDataHeader.get(3), owd); // Header, Child data
		listDataChild.put(listDataHeader.get(4), owf);
		listDataChild.put(listDataHeader.get(5), uof);
		listDataChild.put(listDataHeader.get(6), sp); // Header, Child data
		listDataChild.put(listDataHeader.get(7), aowd);
		listDataChild.put(listDataHeader.get(8), ad);
		listDataChild.put(listDataHeader.get(9), efr); // Header, Child data
		listDataChild.put(listDataHeader.get(10), rd);
		listDataChild.put(listDataHeader.get(11), dm);
	}
	
	

}
