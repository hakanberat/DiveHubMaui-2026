package com.hbd.divehub;



import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;

public class Team extends Activity implements OnItemClickListener{
	ListView listView;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.team);
		
		listView = (ListView) findViewById(R.id.list);
		String[] teamlist = getResources().getStringArray(R.array.teamlist);
		listView.setAdapter(new ArrayAdapter<String>(this, 
                android.R.layout.simple_list_item_1, teamlist)); 
		listView.setOnItemClickListener(this);
				
		
	}
	
	
	@Override
	public void onItemClick(AdapterView<?> arg0, View arg1, int arg2, long arg3) {
		// TODO Auto-generated method stub
		if(arg2 == 0){
			//startActivity(new Intent(Team.this, Ayca.class));
		}
		if(arg2 == 1){
			/*Dialog dhakan = new Dialog(Team.this);
			dhakan.setContentView(R.layout.hakan);
			dhakan.setTitle("K.M.Hakan Kocdag");
			dhakan.show();*/
		}
		if(arg2 == 2){
			/*Dialog dalp = new Dialog(Team.this);
			dalp.setContentView(R.layout.alp);
			dalp.setTitle("Bedri Alp Arat");
			dalp.show();*/
		}
		if(arg2 == 3){
			/*Dialog dburhan = new Dialog(Team.this);
			dburhan.setContentView(R.layout.burhan);
			dburhan.setTitle("Huseyin Burhan San");
			dburhan.show();*/
		}
		if(arg2 == 4){
			/*Dialog dyigit = new Dialog(Team.this);
			dyigit.setContentView(R.layout.yigit);
			dyigit.setTitle("Yigit Gurdal");
			dyigit.show();*/
		}
	}

}
