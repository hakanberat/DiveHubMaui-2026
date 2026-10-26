package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.method.LinkMovementMethod;
import android.view.View;
import android.widget.TextView;

public class Contact extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.contact);
		
		TextView tvdivehub = (TextView) findViewById(R.id.textView8);
		tvdivehub.setMovementMethod(LinkMovementMethod.getInstance());
	}
	
	public void contactform (View oViev){
		startActivity(new Intent(Contact.this,Contactform.class));
	}
	
	
	public void call(View oViev){
		//Intent callIntent = new Intent(Intent.ACTION_CALL);
		//callIntent.setData(Uri.parse("00903923713838"));
		//startActivity(callIntent);
		startActivity(new Intent(Intent.ACTION_CALL, Uri.parse("tel:00903923713838")));
	}
	
	public void call2(View oViev){
		//Intent callIntent = new Intent(Intent.ACTION_CALL);
		//callIntent.setData(Uri.parse("00903923713838"));
		//startActivity(callIntent);
		startActivity(new Intent(Intent.ACTION_CALL, Uri.parse("tel:00905428600119")));
	}
	
	
}
