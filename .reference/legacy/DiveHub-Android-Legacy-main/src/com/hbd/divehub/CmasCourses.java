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

public class CmasCourses extends Activity{
	
	ExpandableListAdapter listAdapter;
	ExpandableListView expListView;
	List<String> listDataHeader;
	HashMap<String, List<String>> listDataChild;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.cmascourses);
		
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
				listDataHeader.add("CMAS 1*");
				listDataHeader.add("CMAS 2**");
				listDataHeader.add("CMAS 3***");
				


				// Adding child data
				List<String> CMAS1  = new ArrayList<String>();
				CMAS1.add("This is the first course available under CMAS to achieve certification as an open water diver. Knowledge of basic diving theory is covered by video and instructor presentations, diving skills are developed in confined water and open water under the supervision of your instructor. By the end of the course you will have knowledge about the SCUBA equipment and dive theory; you will be ready for open water dives. As CMAS 1* Diver you will have theoretical knowledge about: basic gear, basic diving signs, pressure-volume relations, SCUBA implementing, diving physics, diving physiology, first aid, safe diving applications, underwater life and diving tables. You will also gain skills to apply what you have learned in the classroom in theory. Requirements: minimum age is 14 (under age of 18 parent’s approval required), to know how to swim. To be healthy and to sing the notice that there are no health obstacles for Scuba diving (under age of 18 parents approval required), hold at least primary degree in school");
				

				List<String> CMAS2 = new ArrayList<String>();
				CMAS2.add("Why limit yourself with 18 meters and not explore deeper, dive on sunken wrecks, take good underwater pictures as souvenirs, learn to help others or even escape the rat race and dive for a living! In order to improve your diving knowledge and skills - broaden your diving experience by choosing from a variety of CMAS 2* diving courses. During this course you will evaluate your diving skills and learn how to approach and handle critical situations under water and on the surface.During the course you will cover diving related illnesses, first aid and rescue, air consumption calculation, nitrogen absorption, deep diving, the use of decompression tables, dive planning and organization and underwater navigation.After completing the course you will be capable to dive maximum 30 meters with at least a 2 STAR Diver exploring the deeper seas. With an instructor 2 STAR Diver is capable to dive 42 meters for training. You will also become appropriate for night dive and see the night life of underwater which is all hidden during the daytime.Requirements: CMAS 1* certification or equivalent and 20 logged dives after the 1* exam, prof of successful participation in a first aid course including CPR not older than 2 years.Please note: The above course is only available to divers holding a valid diving qualification. PADI Open Water Divers and CMAS 1* Dives are all acceptable diving qualifications for entering to the CMAS 2* Diver Course. Please contact us for full details.");
				

				List<String> CMAS3 = new ArrayList<String>();
				CMAS3.add("As a CMAS 2** diver you have a good level of diving knowledge and experience, but now is the time to take it a step further.Develop your rescue and leading skills to ensure you have the knowledge to help yourself and your buddy! In order to improve your diving knowledge and skills - broaden your diving experience by choosing from a variety of advanced diving courses.Practice the dive accident prevention, self rescue, life saving techniques and emergency management through knowledge development, practical training and emergency scenarios. Plan and manage the dives and manage the water vehicle. Learn everything that makes you a competent diver and dive leader in course of everything related to diving and dive planning.This great fun and rewarding course is essential for those who wish to assist and leading other divers or become a CMAS Instructor.Requirements: CMAS 2** certification and 40 logged dives after passing the 2** course.");
				
				
				

				listDataChild.put(listDataHeader.get(0), CMAS1); // Header, Child data
				listDataChild.put(listDataHeader.get(1), CMAS2);
				listDataChild.put(listDataHeader.get(2), CMAS3);
			}
			
			
	

}
