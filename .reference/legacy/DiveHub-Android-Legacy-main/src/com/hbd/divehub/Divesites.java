package com.hbd.divehub;


import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;

public class Divesites extends Activity implements OnItemClickListener{
	ListView listView;
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.divesites);
		
		listView = (ListView) findViewById(R.id.list);
		String[] divesiteslist = getResources().getStringArray(R.array.divesiteslist);
		listView.setAdapter(new ArrayAdapter<String>(this, 
                android.R.layout.simple_list_item_1, divesiteslist)); 
		listView.setOnItemClickListener(this);
		
	}
	
	
	
	@Override
	public void onItemClick(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
		// TODO Auto-generated method stub
		if(arg2 == 0){
			startActivity(new Intent(Divesites.this, Divehubshore.class));
		}
		if(arg2 == 1){
			startActivity(new Intent(Divesites.this, AmphoraBeach.class));
		}
		if(arg2 == 2){
			startActivity(new Intent(Divesites.this, BogazShipWreck.class));
		}
		if(arg2 == 3){
			startActivity(new Intent(Divesites.this, Alp.class));
		}
		if(arg2 == 4){
			startActivity(new Intent(Divesites.this, Bedri.class));
		}
		if(arg2 == 5){
			startActivity(new Intent(Divesites.this, Pitos.class));
		}
		if(arg2 == 6){
			startActivity(new Intent(Divesites.this, NoahsArk.class));
		}
		if(arg2 == 7){
			startActivity(new Intent(Divesites.this, RayCave.class));
		}
		if(arg2 == 8){
			startActivity(new Intent(Divesites.this, Abdulselam.class));
		}
		if(arg2 == 9){
			startActivity(new Intent(Divesites.this, AmphoraWall.class));
		}
	}

}
