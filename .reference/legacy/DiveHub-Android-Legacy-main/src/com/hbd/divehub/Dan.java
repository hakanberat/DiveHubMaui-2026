package com.hbd.divehub;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.text.method.LinkMovementMethod;
import android.view.View;
import android.widget.TextView;

public class Dan extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.dan);
		TextView tvdan = (TextView) findViewById(R.id.textView4);
		tvdan.setMovementMethod(LinkMovementMethod.getInstance());
	}
	

	public void dancourses (View oViev){
		startActivity(new Intent(Dan.this,Dancourses.class));
	}
	

}
