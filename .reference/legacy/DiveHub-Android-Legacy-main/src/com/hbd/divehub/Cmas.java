package com.hbd.divehub;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.text.method.LinkMovementMethod;
import android.view.View;
import android.widget.TextView;

public class Cmas extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.cmas);
		
		TextView tvcams = (TextView) findViewById(R.id.textView4);
		tvcams.setMovementMethod(LinkMovementMethod.getInstance());
	}
	
	
	public void cmascourses (View oViev){
		startActivity(new Intent(Cmas.this,CmasCourses.class));
	}
	


}
