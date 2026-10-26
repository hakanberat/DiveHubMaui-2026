package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class Services extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.services);
	}
	
	
	public void dives (View oViev){
		startActivity(new Intent(Services.this,Dives.class));
	}
	
	public void courses (View oViev){
		startActivity(new Intent(Services.this,Courses.class));
	}
	
	

}
