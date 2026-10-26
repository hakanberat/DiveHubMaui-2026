package com.hbd.divehub;

import java.util.ArrayList;
import android.app.Activity;
import android.database.Cursor;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

public class Logbooklist extends Activity{
	
	DiveDatabase divehub = new DiveDatabase (this,"db1",null,1);
	
	private String stringid;
	EditText deleteid;
	
	private String Diveno,Divesitename,depth,divetime, date, airIN, airOUT,exposure, weight, leader, buddy, notes;

	ListView lv;

	
	ArrayList<String> listdives;
	ArrayAdapter<String> aa;
		
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.logbooklist);
		
		
				
		lv = (ListView) findViewById(R.id.list);
		
		deleteid = (EditText) findViewById(R.id.editTextdelete);
		
		
			
		
		final ArrayList<String> listdives = new ArrayList<String>();
	    
    	final ArrayAdapter<String> aa;
    	
    	aa = new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,listdives);
    	
    	aa.notifyDataSetChanged();
    	    lv.setAdapter(aa);
    		
    	    
		Cursor c;
		c = divehub.ret();
		if (c.moveToFirst()) {
			while(c.moveToNext()){
				Diveno = c.getString(c.getColumnIndex("diveNO"));
				Divesitename = c.getString(c.getColumnIndex("divesitename"));
				depth = c.getString(c.getColumnIndex("depth"));
				divetime = c.getString(c.getColumnIndex("diveTIME"));
				date = c.getString(c.getColumnIndex("date"));
				airIN = c.getString(c.getColumnIndex("airIN"));
				airOUT = c.getString(c.getColumnIndex("airOUT"));
				exposure = c.getString(c.getColumnIndex("exposure"));
				weight = c.getString(c.getColumnIndex("weight"));
				leader = c.getString(c.getColumnIndex("leader"));
				buddy = c.getString(c.getColumnIndex("buddy"));
				notes = c.getString(c.getColumnIndex("notes"));
									
				listdives.add(" Dive No : "+Diveno+"\n Dive Site Name : "+Divesitename+" \n Depth :  "+depth+" m"+
						"\n divetime : "+divetime+" min"+"\n date : "+date+"\n airIN : "+airIN+" Bar"+
						"\n airOUT : "+airOUT+" Bar"+"\n exposure : "+exposure+" mm"+"\n weight : "+weight+" kg"+
						"\n leader : "+leader+"\n buddy : "+buddy+"\n notes : "+notes);
				
			}
		
		
		}
		else{
			listdives.add("no records in the database");
		    aa.notifyDataSetChanged();
			}
		
	}
	
	public void ref(){
		final ArrayList<String> listdives = new ArrayList<String>();
	    
    	final ArrayAdapter<String> aa;
    	aa = new ArrayAdapter<String>(this,android.R.layout.simple_list_item_1,listdives);
    	    	
    	    lv.setAdapter(aa);
    		aa.notifyDataSetChanged();
    	    
		Cursor c;
		c = divehub.ret();
		if (c.moveToFirst()) {
			while(c.moveToNext()){
				Diveno = c.getString(c.getColumnIndex("diveNO"));
				Divesitename = c.getString(c.getColumnIndex("divesitename"));
				depth = c.getString(c.getColumnIndex("depth"));
				divetime = c.getString(c.getColumnIndex("diveTIME"));
				date = c.getString(c.getColumnIndex("date"));
				airIN = c.getString(c.getColumnIndex("airIN"));
				airOUT = c.getString(c.getColumnIndex("airOUT"));
				exposure = c.getString(c.getColumnIndex("exposure"));
				weight = c.getString(c.getColumnIndex("weight"));
				leader = c.getString(c.getColumnIndex("leader"));
				buddy = c.getString(c.getColumnIndex("buddy"));
				notes = c.getString(c.getColumnIndex("notes"));
								
						
				listdives.add(" Dive No : "+Diveno+"\n Dive Site Name : "+Divesitename+" \n Depth :  "+depth+" m"+
						"\n divetime : "+divetime+" min"+"\n date : "+date+"\n airIN : "+airIN+" Bar"+
						"\n airOUT : "+airOUT+" Bar"+"\n exposure : "+exposure+" mm"+"\n weight : "+weight+" kg"+
						"\n leader : "+leader+"\n buddy : "+buddy+"\n notes : "+notes);
				
			}
		
		}
		
		else{
			listdives.add("no records in the database");
		    aa.notifyDataSetChanged();
			}
	}
	

	public void deletebutton (View oViev){
		
		
		stringid= deleteid.getText().toString().trim();
		
		if(stringid.length()>0)
		{
			DiveDatabase divehub = new DiveDatabase (this,"db1",null,1);

					if(divehub.delete(deleteid.getText().toString()) != 0)
						{
						
							Toast.makeText(getBaseContext(), "Dive is deleted", Toast.LENGTH_LONG).show();
							deleteid.setText("");
							ref();
							}
			
							else
							{				

									listdives.add("no records in the database");
										aa.notifyDataSetChanged();
								}
						
		}
			
			
			
		

			
		else
		{
			
			
			android.app.AlertDialog.Builder ab = new android.app.AlertDialog.Builder(this);
			ab.setTitle("ERROR");
			ab.setMessage("Please enter a valid data");
			ab.setPositiveButton("OK", new android.content.DialogInterface.OnClickListener() {
				
				@Override
				public void onClick(android.content.DialogInterface dialog, int which) {
					// TODO Auto-generated method stub
					dialog.cancel();
				}
			});
			ab.create().show();
			
			
		}	
		
	}
	
   
	
	
	}
	



