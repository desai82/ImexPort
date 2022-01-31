package com.imexportsolutions.imexportsolutions;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.preference.PreferenceManager;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import utility.HttpManager;
import utility.RequestPackage;

public class ReceivedPODetails extends AppCompatActivity implements View.OnClickListener {

    String ID1;
    int FCAID,ICAID,flag=0;
    String Country;
    FloatingActionButton fab;
    Button btnupload,btndownload;
    TextView tvpoid,tvdispatchdate,tvsuppliername,tvfcaname,tvmaterial,tvquantity;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_received_po_details);
        ID1 = getIntent().getStringExtra("ID");
        flag= getIntent().getIntExtra("flag",0);
        SharedPreferences sp = PreferenceManager.getDefaultSharedPreferences(ReceivedPODetails.this);
        Country  = sp.getString("Country",null);
        ICAID  = sp.getInt("ICAID",0);
        FCAID  = sp.getInt("FCAID",0);

        findAllViews();

       fab =  findViewById(R.id.floatingActionButton);
        fab.setOnClickListener(this);

        new FetchPODetails().execute();
    }
    private void findAllViews() {
        tvpoid = findViewById(R.id.tvpoid);
        tvdispatchdate = findViewById(R.id.tvdispatchdate);
        tvsuppliername = findViewById(R.id.tvsuppliername);
        tvfcaname = findViewById(R.id.tvfcaname);
        tvmaterial = findViewById(R.id.tvmaterial);
        tvquantity = findViewById(R.id.tvquantity);
       /* btnupload=findViewById(R.id.btnupload);
        btndownload=findViewById(R.id.btndownload);*/
    }

    public void Download(View view) {
        Intent d=new Intent(ReceivedPODetails.this,DocDownload.class);
        startActivity(d);

    }
    public void uploadIntent(View view) {

        Intent i = new Intent(ReceivedPODetails.this, DocUpload.class);
        i.putExtra("ID",ID1);
        startActivity(i);


    }

    @Override
    public void onClick(View v) {
        if(v == fab){
            if(flag==1) {
                new PORelease().execute();

            }
            else{ Snackbar.make(v, "Upload the Document first ", Snackbar.LENGTH_LONG)
                    .setAction("Action", null).show();

            }
        }
    }


    class FetchPODetails extends AsyncTask<String ,String,String> {
        ProgressDialog pd;
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            pd = new ProgressDialog(ReceivedPODetails.this);
            pd.setTitle("Please Wait");
            pd.setMessage("Loading...");
            pd.setIndeterminate(true);
            pd.setCancelable(false);
            pd.show();
        }


        @Override
        protected String doInBackground(String... strings) {


            RequestPackage rp = new RequestPackage();
            rp.setMethod("GET");
            rp.setUri(HttpManager.URL+"FetchPODetails");
            rp.setParam("ID", ID1);

            String ans = HttpManager.getData(rp);
            return ans.trim();

        }

        @Override
        protected void onPostExecute(String ans) {
            super.onPostExecute(ans);

            String strPO = ans.split("#####--#####")[0];
            String strPOI = ans.split("#####--#####")[1];

            try {
                JSONArray arr1 = new JSONArray(strPO);
                JSONObject obj = arr1.getJSONObject(0);
                tvpoid.setText(obj.getString("PurchaseOrderID"));
                if(Country.equals("INDIA")) {
                    tvdispatchdate.setText(obj.getString("DispatchdatebyFCA").split("#")[0]);
                }else
                {
                    tvdispatchdate.setText(obj.getString("DispatchdatebySupp").split("#")[0]);
                }
                tvfcaname.setText(obj.getString("FCAName"));
                tvsuppliername.setText(obj.getString("SupplierName"));

                JSONArray arr2 = new JSONArray(strPOI);
                JSONObject obj1 = arr2.getJSONObject(0);

                tvquantity.setText(obj1.getString("Quantity"));
                tvmaterial.setText(obj1.getString("MaterialName"));



            } catch (JSONException e) {
                e.printStackTrace();
            }


            pd.dismiss();

        }

    }


    class PORelease extends AsyncTask<String ,String,String> {
        ProgressDialog pd;
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
            pd = new ProgressDialog(ReceivedPODetails.this);
            pd.setTitle("Please Wait");
            pd.setMessage("Loading...");
            pd.setIndeterminate(true);
            pd.setCancelable(false);
            pd.show();
        }


        @Override
        protected String doInBackground(String... strings) {


            if(Country.equals("INDIA")) {
                RequestPackage rp = new RequestPackage();
                rp.setMethod("GET");
                rp.setUri(HttpManager.URL+"POReleaseICA");
                rp.setParam("POID",ID1);
                String ans = HttpManager.getData(rp);
                return ans.trim();
            }
            else{

                RequestPackage rp = new RequestPackage();
                rp.setMethod("GET");
                rp.setUri(HttpManager.URL+"POReleaseFCA");
                rp.setParam("POID",ID1);
                String ans = HttpManager.getData(rp);
                return ans.trim();
            }
        }

        @Override
        protected void onPostExecute(String ans) {
            super.onPostExecute(ans);

         if(ans.equals("Success")){
             Toast.makeText(ReceivedPODetails.this, "Your Order is Released", Toast.LENGTH_SHORT).show();
         }


            pd.dismiss();

        }

    }


}
