package com.hbd.divehub;



import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AnimationUtils;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.Gallery;
import android.widget.ImageSwitcher;
import android.widget.ImageView;
import android.widget.ViewSwitcher;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.Gallery.LayoutParams;
import android.widget.ViewSwitcher.ViewFactory;

@SuppressWarnings("deprecation")
public class Galleryy extends Activity implements
AdapterView.OnItemSelectedListener, ViewSwitcher.ViewFactory{

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.gallery);
		
		mSwitcher = (ImageSwitcher) findViewById(R.id.switcher);
	    mSwitcher.setFactory((ViewFactory) this);
	    mSwitcher.setInAnimation(AnimationUtils.loadAnimation(this,
	            android.R.anim.fade_in));
	    mSwitcher.setOutAnimation(AnimationUtils.loadAnimation(this,
	            android.R.anim.fade_out));

	    Gallery g = (Gallery) findViewById(R.id.gallery);
	    g.setAdapter(new ImageAdapter(this));
	    g.setOnItemSelectedListener((OnItemSelectedListener) this);
	}
	 public void onItemSelected(AdapterView<?> parent, View v, int position, long id) {
	        mSwitcher.setImageResource(mImageIds[position]);
	    }
	 public void onNothingSelected(AdapterView<?> parent) {
	    }

	    public View makeView() {
	        ImageView i = new ImageView(this);
	        //i.setBackgroundColor(0xFF000000);
	        i.setScaleType(ImageView.ScaleType.FIT_CENTER);
	        i.setLayoutParams(new ImageSwitcher.LayoutParams(LayoutParams.MATCH_PARENT,LayoutParams.MATCH_PARENT));
	        return i;
	    }

	    private ImageSwitcher mSwitcher;

	    public class ImageAdapter extends BaseAdapter {
	        public ImageAdapter(Context c) {
	            mContext = c;
	        }

	        public int getCount() {
	            return mThumbIds.length;
	        }

	        public Object getItem(int position) {
	            return position;
	        }

	        public long getItemId(int position) {
	            return position;
	        }

	        public View getView(int position, View convertView, ViewGroup parent) {
	            ImageView i = new ImageView(mContext);

	            i.setImageResource(mThumbIds[position]);
	            i.setAdjustViewBounds(true);
	            i.setLayoutParams(new Gallery.LayoutParams(
	                    LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT));
	            i.setBackgroundResource(R.drawable.abdulselam);
	            return i;
	        }

	        private Context mContext;

	    }

	    private Integer[] mThumbIds = {
	    		R.drawable.pic1,
	            R.drawable.pic2, 
	            R.drawable.pic3,
	            R.drawable.pic4,
	            R.drawable.pic5,
	            R.drawable.pic7,
	            R.drawable.pic8,
	            R.drawable.pic10, 
	            R.drawable.pic11,
	            R.drawable.pic12,
	            R.drawable.pic15,
	            R.drawable.pic16,
	            R.drawable.am2, 
	            R.drawable.d1,
	            R.drawable.d3,
	            R.drawable.d4,
	            R.drawable.d7,
	            R.drawable.d9,
	            R.drawable.wr1,
	            R.drawable.wr2,
	            R.drawable.ray2,
	            R.drawable.cr1,
	            R.drawable.ani8,
	            R.drawable.ani9,
	            R.drawable.ani12,
	            R.drawable.ani16
	            
	       
	            
	            };

	    private Integer[] mImageIds = {
	    		R.drawable.pic1,
	            R.drawable.pic2, 
	            R.drawable.pic3,
	            R.drawable.pic4,
	            R.drawable.pic5,
	            R.drawable.pic7,
	            R.drawable.pic8,
	            R.drawable.pic10, 
	            R.drawable.pic11,
	            R.drawable.pic12,
	            R.drawable.pic15,
	            R.drawable.pic16,
	            R.drawable.am2, 
	            R.drawable.d1,
	            R.drawable.d3,
	            R.drawable.d4,
	            R.drawable.d7,
	            R.drawable.d9,
	            R.drawable.wr1,
	            R.drawable.wr2,
	            R.drawable.ray2,
	            R.drawable.cr1,
	            R.drawable.ani8,
	            R.drawable.ani9,
	            R.drawable.ani12,
	            R.drawable.ani16
		            };
		
	}


