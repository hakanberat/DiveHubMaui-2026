package com.hbd.divehub;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ExpandableListView;
import android.widget.ExpandableListView.OnChildClickListener;
import android.widget.ExpandableListView.OnGroupClickListener;
import android.widget.ExpandableListView.OnGroupCollapseListener;
import android.widget.ExpandableListView.OnGroupExpandListener;

public class Dancourses extends Activity{
	
	ExpandableListAdapter listAdapter;
	ExpandableListView expListView;
	List<String> listDataHeader;
	HashMap<String, List<String>> listDataChild;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.dancourses);
		
		// get the listview
				expListView = (ExpandableListView) findViewById(R.id.lvExp);

				// preparing list data
				prepareListData();

				listAdapter = new ExpandableListAdapter(this, listDataHeader, listDataChild);

				// setting list adapter
				expListView.setAdapter(listAdapter);

				// Listview Group click listener
				expListView.setOnGroupClickListener(new OnGroupClickListener() {

					@Override
					public boolean onGroupClick(ExpandableListView parent, View v,
							int groupPosition, long id) {
						// Toast.makeText(getApplicationContext(),
						// "Group Clicked " + listDataHeader.get(groupPosition),
						// Toast.LENGTH_SHORT).show();
						return false;
					}
				});

				// Listview Group expanded listener
				expListView.setOnGroupExpandListener(new OnGroupExpandListener() {

					@Override
					public void onGroupExpand(int groupPosition) {
						/*Toast.makeText(getApplicationContext(),
								listDataHeader.get(groupPosition) + " Expanded",
								Toast.LENGTH_SHORT).show();*/
					}
				});

				// Listview Group collasped listener
				expListView.setOnGroupCollapseListener(new OnGroupCollapseListener() {

					@Override
					public void onGroupCollapse(int groupPosition) {
						/*Toast.makeText(getApplicationContext(),
								listDataHeader.get(groupPosition) + " Collapsed",
								Toast.LENGTH_SHORT).show();*/

					}
				});

				// Listview on child click listener
				expListView.setOnChildClickListener(new OnChildClickListener() {

					@Override
					public boolean onChildClick(ExpandableListView parent, View v,
							int groupPosition, int childPosition, long id) {
						// TODO Auto-generated method stub
						/*Toast.makeText(
								getApplicationContext(),
								listDataHeader.get(groupPosition)
										+ " : "
										+ listDataChild.get(
												listDataHeader.get(groupPosition)).get(
												childPosition), Toast.LENGTH_SHORT)
								.show();*/
						return false;
					}
				});
		}
			private void prepareListData() {
				listDataHeader = new ArrayList<String>();
				listDataChild = new HashMap<String, List<String>>();

				// Adding child data
				listDataHeader.add("Oxygen Provider");
				listDataHeader.add("Basic life Support (BLS)");
				


				// Adding child data
				List<String> op  = new ArrayList<String>();
				op.add("Most injured divers are breathing and require supplemental oxygen. The Oxygen Provider Module is designed to teach the skills needed to perform this task safely and effectively. In addition this course will introduce novice divers to the fundamentals of dive accident recognition, response and management. This program provides an excellent training opportunity for divers and continuing education for leadership-level divers and instructors.Prerequisites: Must be a diver.Minimum age: 14,Course duration: 4 hours,Course dates: On request or in conjunction with CMAS 2**, CMAS 3** course.");
				
				List<String> bls  = new ArrayList<String>();
				bls.add("The course will not only train divers and non-divers to resuscitate an injured person with a circulatory arrest, but can also prevent a person from getting in that condition. External bleeding, choking and shock can lead to severe circulatory and respiratory problems. The DAN BLS course will prepare you to react in the correct way, when accidents happen.he Basic Life Support course, represents entry-level training designed to educate persons in providing Basic Life Support techniques to adult victims with life threatening injuries, while activating the local medical services.The DAN Basic Life Support provider (DAN BLS Provider) program is designed to teach the knowledge and skills needed to provide Basic Life");
				

				
				
				

				listDataChild.put(listDataHeader.get(0), op); // Header, Child data
				listDataChild.put(listDataHeader.get(1), bls);
			}
				
	

}
