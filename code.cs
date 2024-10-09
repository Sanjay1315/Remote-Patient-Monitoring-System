using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
public partial class userroles : System.Web.UI.Page
{
SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["constr"].ToString());
protected void Page_Load(object sender, EventArgs e)
{
if (!Page.IsPostBack) data();
}
private void data()
{
if (cn.State == ConnectionState.Closed) cn.Open();
SqlDataAdapter da = new SqlDataAdapter("select * from tblPatient", cn);
DataSet ds = new DataSet();
da.Fill(ds);
GridView1.DataSource = ds.Tables[0];
GridView1.DataBind();
}
protected void Button1_Click(object sender, EventArgs e)
{

}
protected void Button1_Click1(object sender, EventArgs e)
{
if (cn.State == ConnectionState.Closed) cn.Open();
SqlCommand cmd = new SqlCommand("insert into tblPatient values ('" + TextBox2.Text + "','" + TextBox3.Text + "','" + DropDownList1.Text + "','" + TextBox7.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "')", cn);
cmd.ExecuteNonQuery();
SqlCommand cmdmax = new SqlCommand("select max(patid) from tblPatient", cn);
int imax = Convert.ToInt32(cmdmax.ExecuteScalar());
string directoryPath = Server.MapPath(string.Format("~/uploads/{0}/", imax));
if (!Directory.Exists(directoryPath))
{
Directory.CreateDirectory(directoryPath);
}

data();
}
protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
{
data();
GridView1.EditIndex = e.NewEditIndex;
GridView1.DataBind();
TextBox txt = new  TextBox();
txt = (TextBox) GridView1.Rows[e.NewEditIndex].Cells[3].Controls[0];
//txt.Enabled = false;
//TextBox txt1 = new TextBox();
//txt1 = (TextBox) GridView1.Rows[e.NewEditIndex].Cells[4].Controls[0];
//txt1.Enabled = false;
}
protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
{
string strQry;
string str ;
str = GridView1.Rows[e.RowIndex].Cells[2].Text;
strQry = "delete from tblPatient where patid=" + str + "";
SqlCommand cmd =new SqlCommand(strQry, cn);
if (cn.State == ConnectionState.Closed) cn.Open();
cmd.ExecuteNonQuery();
Response.Write("<Script>alert('Record Deleted');</script>");
cn.Close();
data();
GridView1.EditIndex = -1;
GridView1.DataBind();
}
protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
{
data();
GridView1.EditIndex = -1;
GridView1.DataBind();
}
protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
{
string strQry;
string str;
TextBox txtid = (TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0];
str = txtid.Text;
TextBox txtname = (TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0];
TextBox txtdob = (TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0];
TextBox txtaddress = (TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0];
TextBox txtgender = (TextBox)GridView1.Rows[e.RowIndex].Cells[6].Controls[0];
TextBox txtphone = (TextBox)GridView1.Rows[e.RowIndex].Cells[7].Controls[0];
TextBox txtemail = (TextBox)GridView1.Rows[e.RowIndex].Cells[8].Controls[0];
TextBox txtdisease = (TextBox)GridView1.Rows[e.RowIndex].Cells[9].Controls[0];

strQry = "update tblPatient set patname='" + txtname.Text + "',dob='" + txtdob.Text + "',gender='" + txtgender.Text + "',address='" + txtaddress.Text + "',phone='" + txtphone.Text + "', email='" + txtemail.Text + "',disease='" + txtdisease.Text + "' where patid=" + str + "";
SqlCommand cmd = new SqlCommand(strQry, cn);
if (cn.State == ConnectionState.Closed) cn.Open();
cmd.ExecuteNonQuery();
Response.Write("<Script>alert('Record Updated');</script>");
cn.Close();
data();
GridView1.EditIndex = -1;
GridView1.DataBind();
}
}

Login activity

package in.nanda.remotepatient;


import in.nanda.remotepatient.R;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class LoginActivity extends Activity {

private Button btn_Login;
private Button btn_Back;
SQLiteDatabase con;

public EditText e1;
public EditText e2;
public String userid="";
public String pwd="";
@Override
protected void onCreate(Bundle savedInstanceState) {
super.onCreate(savedInstanceState);
setContentView(R.layout.activity_login);

con =  this.openOrCreateDatabase("CBAC", MODE_PRIVATE, null);
e1=(EditText) findViewById(R.id.editText1);
e2=(EditText) findViewById(R.id.editText2);

btn_Login= (Button) findViewById(R.id.button1);
btn_Login.setOnClickListener(new OnClickListener() {
public void onClick(View arg0) {
userid = e1.getText().toString();
pwd = e2.getText().toString();


if(userid.equals("admin") && pwd.equals("admin"))
{
Toast.makeText(LoginActivity.this, "Login Success.", Toast.LENGTH_SHORT).show();
Intent i= new Intent(LoginActivity.this, ConnectActivity.class);
startActivityForResult(i, 500);
// overridePendingTransition(R.anim.slide_in_right, R.anim.slide_out_left);
con.close();
finish();
}
else
Toast.makeText(LoginActivity.this, "Wrong Password or Mobile number.", Toast.LENGTH_SHORT).show();



}
}) ;



btn_Back= (Button) findViewById(R.id.button2);
btn_Back.setOnClickListener(new OnClickListener() {
public void onClick(View arg0) {
Intent i = new Intent(LoginActivity.this,MainActivity.class);
startActivityForResult(i, 500);
//	overridePendingTransition(R.anim.slide_in_right, R.anim.slide_out_left);
}
}) ;
}



}

Monitoring

package in.nanda.remotepatient;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

import android.R.string;
import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

public class MonitorActivity extends Activity {

private ImageView imageView;
private TextView tv;
private Button btn;
private LinearLayout inScrollView;
String strPatid ;
URL url;
@Override
protected void onCreate(Bundle savedInstanceState) {
super.onCreate(savedInstanceState);
setContentView(R.layout.activity_monitor);

Intent intent = getIntent();
tv = (TextView) findViewById(R.id.textView1);
strPatid = intent.getStringExtra("patid");
strPatid = strPatid.substring(0,strPatid.indexOf("-"));
tv.setText("Patient ID : " + strPatid);

btn = (Button) findViewById(R.id.button1);
btn.setOnClickListener(new OnClickListener() {

@Override
public void onClick(View arg0) {
// TODO Auto-generated method stub


String strFileList = WebService.getFilelist(strPatid.trim(), "getFilelist");
String[] arrFileList = strFileList.split(",");
LinearLayout lout = (LinearLayout) findViewById(R.id.linearLayout1);
//  Toast.makeText(MonitorActivity.this, arrFileList[0].toString(), Toast.LENGTH_SHORT).show();

for(int x=0;x<=arrFileList.length-1;x++) {
URL newurl=null;
try {
newurl = new URL("http://192.168.43.96:32768/uploads/"+strPatid.trim()+"/"+arrFileList[x].toString());
//newurl = new URL("http://192.168.8.1:32768/uploads/1/1013.jpg");
} catch (MalformedURLException e) {
// TODO Auto-generated catch block
e.printStackTrace();
}
Bitmap mIcon_val=null;
try {
mIcon_val = BitmapFactory.decodeStream(newurl.openConnection() .getInputStream());
} catch (IOException e) {
// TODO Auto-generated catch block
e.printStackTrace();
}

ImageView image = new ImageView(MonitorActivity.this);
image.setImageBitmap(mIcon_val);

lout.addView(image);

}
}
});


}


}

Webservice

package in.nanda.remotepatient;

import in.nanda.remotepatient.R;


import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.preference.EditTextPreference;

import android.view.View;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.TextView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import android.app.Activity;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.TextView;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.PropertyInfo;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;
import org.w3c.dom.Text;
import java.lang.String;
import android.os.StrictMode;
import java.util.*;
import java.util.concurrent.ExecutionException;

import android.widget.DatePicker.OnDateChangedListener;
import android.widget.Toast;

public class WebService {
//Namespace of the Webservice - can be found in WSDL
private static String NAMESPACE = "http://tempuri.org/";
//Webservice URL - WSDL File location
private static String URL = "http://192.168.43.96:32768/Service.asmx";//Make sure you changed IP address
//SOAP Action URI again Namespace + Web method name
private static String SOAP_ACTION = "http://tempuri.org/";


public static String downloadfile(String paln_name,String webmethod)
{
String loginStatus ="";
SoapObject request = new SoapObject(NAMESPACE,webmethod);
PropertyInfo P_Planname = new PropertyInfo();
P_Planname.setName("rolename");
P_Planname.setValue(paln_name);
P_Planname.setType(String.class);
request.addProperty(P_Planname);

//String webmethodname="requeststr";

SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
envelope.dotNet = true;
envelope.setOutputSoapObject(request);
HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
//AndroidHttpTransport androidHttpTransport = new AndroidHttpTransport(URL);
try
{
androidHttpTransport.call(SOAP_ACTION + webmethod, envelope);
SoapPrimitive response = (SoapPrimitive) envelope.getResponse();
loginStatus = response.toString();
} catch (Exception e)
{
e.printStackTrace();

}
return loginStatus;
//	return "hai";
}

public static String getFilelist(String patid,String webmethod)
{
String loginStatus ="";
SoapObject request = new SoapObject(NAMESPACE,webmethod);
PropertyInfo P_Planname = new PropertyInfo();
P_Planname.setName("patid");
P_Planname.setValue(patid);
P_Planname.setType(String.class);
request.addProperty(P_Planname);

//String webmethodname="requeststr";

SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
envelope.dotNet = true;
envelope.setOutputSoapObject(request);
HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
//AndroidHttpTransport androidHttpTransport = new AndroidHttpTransport(URL);
try
{
androidHttpTransport.call(SOAP_ACTION + webmethod, envelope);
SoapPrimitive response = (SoapPrimitive) envelope.getResponse();
loginStatus = response.toString();
} catch (Exception e)
{
e.printStackTrace();

}
return loginStatus;
//	return "hai";
}
public static String invokeLoginWS(String paln_name,String age,String term,String insAmount,String webmethod)
{
String loginStatus ="";
SoapObject request = new SoapObject(NAMESPACE,webmethod);
PropertyInfo P_Planname = new PropertyInfo();
PropertyInfo P_age = new PropertyInfo();
PropertyInfo P_term = new PropertyInfo();
PropertyInfo P_insAmount = new PropertyInfo();

P_Planname.setName("planname");
P_Planname.setValue(paln_name);
P_Planname.setType(String.class);
request.addProperty(P_Planname);

P_age.setName("age");
P_age.setValue(age);
P_age.setType(String.class);
request.addProperty(P_age);

P_term.setName("term");
P_term.setValue(term);
P_term.setType(String.class);
request.addProperty(P_term);

P_insAmount.setName("insAmount");
P_insAmount.setValue(insAmount);
P_insAmount.setType(String.class);
request.addProperty(P_insAmount);
//String webmethodname="requeststr";

SoapSerializationEnvelope envelope = new SoapSerializationEnvelope(SoapEnvelope.VER11);
envelope.dotNet = true;
envelope.setOutputSoapObject(request);
HttpTransportSE androidHttpTransport = new HttpTransportSE(URL);
//AndroidHttpTransport androidHttpTransport = new AndroidHttpTransport(URL);
try
{
androidHttpTransport.call(SOAP_ACTION + webmethod, envelope);
SoapPrimitive response = (SoapPrimitive) envelope.getResponse();
loginStatus = response.toString();
} catch (Exception e)
{
e.printStackTrace();

}
return loginStatus;
//	return "hai";
}
}