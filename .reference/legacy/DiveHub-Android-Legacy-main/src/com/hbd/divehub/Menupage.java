package com.hbd.divehub;

import android.app.Activity;
import android.app.Dialog;
import android.content.Intent;
import android.os.Bundle;

import android.view.View;

public class Menupage extends Activity{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.menupage);
	}
	
	
	public void about (View oViev){
		startActivity(new Intent(Menupage.this,About.class));
	}
	
	public void divesites (View oViev){
		startActivity(new Intent(Menupage.this,Divesites.class));
	}
	
	public void logbook (View oViev){
		
		//startActivity(new Intent(Menupage.this,Logbook.class));
		Dialog logbookinfo = new Dialog(Menupage.this);
		logbookinfo.setContentView(R.layout.logbookinfo);
		logbookinfo.setTitle("LogBook");
		logbookinfo.show();
	}
	
	
	
	public void contact (View oViev){
		startActivity(new Intent(Menupage.this,Contact.class));
	}
	
	public void services (View oViev){
		startActivity(new Intent(Menupage.this,Services.class));
	}
	public void infor (View oViev){
		Dialog ddinfo = new Dialog(Menupage.this);
    	ddinfo.setContentView(R.layout.appinfo);
		ddinfo.setTitle("App Information");
		ddinfo.show();
	}
	
	public void gallery (View oViev){
		startActivity(new Intent(Menupage.this, Galleryy.class));
	}
	
	
	

}
