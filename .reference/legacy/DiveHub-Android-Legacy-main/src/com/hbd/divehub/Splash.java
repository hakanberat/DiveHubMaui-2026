package com.hbd.divehub;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.view.Window;

public class Splash extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		requestWindowFeature(Window.FEATURE_NO_TITLE);
		setContentView(R.layout.splash);
		setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
		
		Thread splash_screen = new Thread(){
			public void run(){
			 try{
				 sleep(5000);
			 }catch(Exception e){
				 e.printStackTrace();
			 }finally{
				 startActivity(new Intent(getApplicationContext(),MainActivity.class));
				 finish();
			 }
			}
		};
		splash_screen.start();

}
}
