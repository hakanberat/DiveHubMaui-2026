package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class Courses extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.courses);
	}
	
	public void padi (View oViev){
		startActivity(new Intent(Courses.this,Padi.class));
	}
	public void cmas (View oViev){
		startActivity(new Intent(Courses.this,Cmas.class));
	}
	public void dan (View oViev){
		startActivity(new Intent(Courses.this,Dan.class));
	}
	


}
