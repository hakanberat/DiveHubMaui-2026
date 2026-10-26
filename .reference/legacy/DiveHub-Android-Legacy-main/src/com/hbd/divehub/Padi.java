package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.text.method.LinkMovementMethod;
import android.view.View;
import android.widget.TextView;

public class Padi extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.padi);
		
		TextView tvpadi = (TextView) findViewById(R.id.textView4);
		tvpadi.setMovementMethod(LinkMovementMethod.getInstance());
	}
	
	public void padicourses (View oViev){
		startActivity(new Intent(Padi.this,Padicourses.class));
	}
	
	
}
