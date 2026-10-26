package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class Logbooknew extends Activity{
	
	DiveDatabase divehub = new DiveDatabase (this,"db1",null,1);
	
	private String Diveno,Divesitename,depth,divetime, date, airIN, airOUT,exposure, weight, leader, buddy, notes;
	
	EditText etDiveno,etDivesitename,etdepth,etdivetime, etdate, etairIN, etairOUT,etexposure, etweight, etleader,etbuddy,etnotes;
	
			
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.logbook);
		
	 
		 etDiveno = (EditText) findViewById(R.id.editTextDiveNo);
		 etDivesitename = (EditText) findViewById(R.id.editTextDiveSiteName);
		 etdepth = (EditText) findViewById(R.id.editTextDepth);
		 etdivetime  = (EditText) findViewById(R.id.editTextDiveTime);
		 etdate = (EditText) findViewById(R.id.editTextDate); 
		 etairIN = (EditText) findViewById(R.id.editTextAirIn); 
		 etairOUT = (EditText) findViewById(R.id.editTextAirOut);
		 etexposure = (EditText) findViewById(R.id.editTextExposure); 
		 etweight = (EditText) findViewById(R.id.editTextWeight); 
		 etleader = (EditText) findViewById(R.id.editTextLeader);
		 etbuddy = (EditText) findViewById(R.id.editTextBuddy);
		 etnotes = (EditText) findViewById(R.id.editTextNotes);
	}
	
	public void savebutton (View oViev){
		
			
		 Diveno = etDiveno.getText().toString().trim();
		 Divesitename = etDivesitename.getText().toString().trim();
		 depth = etdepth.getText().toString().trim();
		 divetime = etdivetime.getText().toString().trim(); 
		 date = etdate.getText().toString().trim();
		 airIN = etairIN.getText().toString().trim(); 
		 airOUT = etairOUT.getText().toString().trim();
		 exposure = etexposure.getText().toString().trim(); 
		 weight = etweight.getText().toString().trim(); 
		 leader = etleader.getText().toString().trim(); 
		 buddy = etbuddy.getText().toString().trim(); 
		 notes = etnotes.getText().toString().trim();
		
		 if(Diveno.length()>0)
		 {
		
			if(divehub.add(Diveno, Divesitename, depth, divetime, date, airIN, airOUT, exposure, weight, leader, buddy, notes)!=1)
			
		
		{
				Toast.makeText(getBaseContext(), "Data Inserted", Toast.LENGTH_LONG).show();
				startActivity(new Intent(Logbooknew.this,Logbooklist.class));
				/*etDiveno.setText("");
				etDivesitename.setText("");
				etdepth.setText("");
				etdivetime.setText("");
				etdate.setText("");
				etairIN.setText("");
				etairOUT.setText("");
				etexposure.setText("");
				etweight.setText("");
				etleader.setText("");
				etbuddy.setText("");
				etnotes.setText("");*/
				
				startActivity(new Intent(Logbooknew.this,Logbooklist.class));
				
			}
			else
			{
				Toast.makeText(this,"ERROR! NOT Added, Try Again!",Toast.LENGTH_SHORT).show();
			}
			
				
			
		 }
		 else
		 {
			 android.app.AlertDialog.Builder ab = new android.app.AlertDialog.Builder(this);
				ab.setTitle("ERROR");
				ab.setMessage("Please enter a valid data for DIVE NO");
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
