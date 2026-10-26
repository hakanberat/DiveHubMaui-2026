package com.hbd.divehub;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.database.sqlite.SQLiteDatabase.CursorFactory;

public class DiveDatabase extends SQLiteOpenHelper {
	
	

	public DiveDatabase(Context context, String name, CursorFactory factory,
			int version) {
		super(context, name, factory, version);
		// TODO Auto-generated constructor stub
	}

	@Override
	public void onCreate(SQLiteDatabase db) {
		// TODO Auto-generated method stub
		String CREATE_TABLE = "CREATE TABLE logbook(diveNO text primary key,divesitename text,depth text,diveTIME text,date text,airIN text,airOUT text,exposure text,weight text,leader text,buddy text,notes text)";
		db.execSQL(CREATE_TABLE);
	}


	@Override
	public void onUpgrade(SQLiteDatabase db, int arg1, int arg2) {
		// TODO Auto-generated method stub
		db.execSQL("DROP TABLE IF EXIST logbook");
		onCreate(db);}
		
	
	public long add(String a1, String a2, String a3, String a4, String a5, String a6, String a7, String a8, String a9,
			 String a10, String a11, String a12)
	{
		SQLiteDatabase db= this.getWritableDatabase();
		ContentValues cv = new ContentValues();
		
		
		cv.put("diveNO", a1);
		cv.put("divesitename", a2);
		cv.put("depth", a3);
		cv.put("diveTIME", a4);
		cv.put("date", a5);
		cv.put("airIN", a6);
		cv.put("airOUT", a7);
		cv.put("exposure", a8);
		cv.put("weight", a9);
		cv.put("leader", a10);
		cv.put("buddy", a11);
		cv.put("notes", a12);
		
		
		return db.insert("logbook", null, cv);		
	}
	
	
	
	public int delete(String a1)
	{
		SQLiteDatabase db = this.getWritableDatabase();
		return db.delete("logbook","diveNO= '"+a1+"' ",null);
		
	}
	
   public Cursor ret()
	
	{
		SQLiteDatabase db = this.getWritableDatabase();
		Cursor c = db.rawQuery("select * from logbook", null);
		return c;
	}
	
}



