package com.hbd.divehub;

import java.util.Arrays;
import java.util.List;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class Contactform extends Activity{
	
	int code;
	EditText name, email, msg, phone;
	String stringname, stringemail, stringmsg, stringphone, mailbody="";

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.contactform);
		
		 name = (EditText) findViewById(R.id.editTextNAME);
	     email = (EditText) findViewById(R.id.editTextEMAIL);
	     phone = (EditText) findViewById(R.id.editTextPHONE);
	     msg = (EditText) findViewById(R.id.editTextMESSAGE);
	}
	
	public void sendmsg (View oViev){
		
		stringname = name.getText().toString().trim();
		stringemail= email.getText().toString().trim();
		stringphone= phone.getText().toString().trim();
		stringmsg = msg.getText().toString().trim();
		mailbody = " Name : "+stringname+"\n eMail : "+stringemail+"\n Phone : "+stringphone+"\n Message : "+stringmsg;
		
		if(stringname.length()>0 && stringemail.length()>0 && stringphone.length()>0 && stringmsg.length()>0)
		{
			
			ConnectivityManager connMgr = (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
			 NetworkInfo networkInfo = connMgr.getActiveNetworkInfo();
			 
			 if (networkInfo != null && networkInfo.isConnected()) 
			 {
				 Log.i("SendMailActivity", "Send Button Clicked.");

					String fromEmail = "hbdtest1@gmail.com";
					
					String fromPassword = " ";
					
					String toEmails = "hakanberatdemircan@gmail.com";
					
					List<String> toEmailList = Arrays.asList(toEmails
							.split("\\s*,\\s*"));
					
					Log.i("SendMailActivity", "To List: " + toEmailList);
					
					String emailSubject = "test subject";
					
					mailbody = " Name : "+stringname+"\n eMail "+stringemail+"\n Phone :"+stringphone+"\n Message :"+stringmsg;
					
					new SendMailTask(Contactform.this).execute(fromEmail,
							fromPassword, toEmailList, emailSubject, mailbody);	
			 }
			 else 
			 {
			    	Toast.makeText(getApplicationContext(), "Please Check Your Internet Connection", Toast.LENGTH_LONG).show();
			 }
			
			
				 
		}
		else
		{
			AlertDialog.Builder ab = new AlertDialog.Builder(Contactform.this);
			ab.setMessage("Please Fill All the Areas!");
			ab.setCancelable(false);
			ab.setPositiveButton("OK", new DialogInterface.OnClickListener() {
				
				@Override
				public void onClick(DialogInterface dialog, int which) {
					// TODO Auto-generated method stub
					dialog.cancel();
				}
			});
			
			ab.create().show();
		}
	}

}
