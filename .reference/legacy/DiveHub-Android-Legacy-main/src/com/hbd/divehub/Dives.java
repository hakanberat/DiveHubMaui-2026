package com.hbd.divehub;
import android.app.Activity;
import android.app.Dialog;
import android.os.Bundle;

import android.view.View;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class Dives extends Activity implements OnItemClickListener{
	
	ListView listView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.dives);
		
		listView = (ListView) findViewById(R.id.list);
		String[] divelist = getResources().getStringArray(R.array.divelist);
		listView.setAdapter(new ArrayAdapter<String>(this, 
                android.R.layout.simple_list_item_1, divelist)); 
		listView.setOnItemClickListener(this);
		
		
		
	}
	
	@Override
	public void onItemClick(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
		// TODO Auto-generated method stub
		if(arg2 == 0){
			Dialog ddive = new Dialog(Dives.this);
			ddive.setContentView(R.layout.dbdives);
			ddive.setTitle("Dives");
			ddive.show();
		}
		if(arg2 == 1){
			Dialog dtrydive = new Dialog(Dives.this);
			dtrydive.setContentView(R.layout.dbtrydives);
			dtrydive.setTitle("Try Dives");
			dtrydive.show();
		}
		if(arg2 == 2){
			Dialog dshoredive = new Dialog(Dives.this);
			dshoredive.setContentView(R.layout.dbshoredive);
			dshoredive.setTitle("Shore Dives");
			dshoredive.show();
		}
		if(arg2 == 3){
			Dialog dboatdives = new Dialog(Dives.this);
			dboatdives.setContentView(R.layout.dbboatdives);
			dboatdives.setTitle("Boat Dives");
			dboatdives.show();
		}
		if(arg2 == 4){
			Dialog dnightdives = new Dialog(Dives.this);
			dnightdives.setContentView(R.layout.dbnightdives);
			dnightdives.setTitle("Night Dives");
			dnightdives.show();
		}
	}
	
	
	

	
}
