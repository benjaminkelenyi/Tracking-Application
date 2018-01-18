package com.example.benjaminkelenyi.myapplicationtrackgps;

import android.app.AlertDialog;
import android.content.Context;
import android.os.AsyncTask;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLEncoder;

/**
 * Created by Benjamin Kelenyi on 1/4/2018.
 */

public class BackgroundWorker  extends AsyncTask<String, Void, String>{
    Context context;
    AlertDialog alertDialog;
    BackgroundWorker (Context ctx) { context = ctx;}

    @Override
    protected String doInBackground(String... params) {
        String type = params [0];
        String reg_url="http://192.168.43.184/register.php";
        if(type.equals("post")) {
            try
            {
                String deviceid = params[1];
                String date = params[2];
                String time = params[3];
                String latitude = params[4];
                String longitude = params[5];
                URL url = new URL(reg_url);
                HttpURLConnection httpURLConnection =(HttpURLConnection)url.openConnection();
                httpURLConnection.setRequestMethod("POST");
                httpURLConnection.setDoOutput(true);
                httpURLConnection.setDoInput(true);
                OutputStream OS = httpURLConnection.getOutputStream();
                BufferedWriter bufferedWriter = new BufferedWriter(new OutputStreamWriter(OS, "UTF-8"));
                String post_data = URLEncoder.encode("DeviceID", "UTF-8")+"="+URLEncoder.encode(deviceid, "UTF-8")+"&"
                        +URLEncoder.encode("Date", "UTF-8")+"="+URLEncoder.encode(date, "UTF-8")+"&"
                        +URLEncoder.encode("Time", "UTF-8")+"="+URLEncoder.encode(time, "UTF-8")+"&"
                        +URLEncoder.encode("Latitude", "UTF-8")+"="+URLEncoder.encode(latitude, "UTF-8")+"&"
                        +URLEncoder.encode("Longitude", "UTF-8")+"="+URLEncoder.encode(longitude, "UTF-8");
                bufferedWriter.write(post_data);
                bufferedWriter.flush();
                bufferedWriter.close();
                InputStream IS = httpURLConnection.getInputStream();
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(IS, "iso-8859-1"));
                String result="";
                String line="";
                while ((line=bufferedReader.readLine())!=null) {
                    result+=line;
                }
                bufferedReader.close();
                IS.close();
                httpURLConnection.disconnect();
                return result;
            } catch (ProtocolException e) {
                e.printStackTrace();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        return null;
    }

    @Override
    protected void onPreExecute() {
        alertDialog = new AlertDialog.Builder(context).create();
    }

    @Override
    protected void onPostExecute(String result) {
        alertDialog.setMessage(result);
        alertDialog.show();
    }

    @Override
    protected void onProgressUpdate(Void... values) {
        super.onProgressUpdate(values);
    }
}
