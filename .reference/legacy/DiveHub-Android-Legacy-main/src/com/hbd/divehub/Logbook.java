package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class Logbook extends Activity{
	
	DiveDatabase divehub = new DiveDatabase (this,"db1",null,1);

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.logbookmain);
	}
	
	public void newdive (View oViev){
		startActivity(new Intent(Logbook.this,Logbooknew.class));
	}

	
	public void listdives (View oViev){
		startActivity(new Intent(Logbook.this,Logbooklist.class));
	}
}
