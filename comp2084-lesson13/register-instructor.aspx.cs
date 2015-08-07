using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//cryptography library reference
using System.Security.Cryptography;

//reference ef model
using comp2084_lesson13.Models;

namespace comp2084_lesson13
{
    public partial class register_instructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //populate departments on first load of the page ONLY
            if (!IsPostBack)
            {
                GetDepartments();
            }
        }

        protected void GetDepartments()
        {
            using (DefaultConnection db = new DefaultConnection())
            {
                //get departments
                var deps = from d in db.Departments
                           orderby d.Name
                           select d;

                //populate dropdown
                ddlDepartment.DataSource = deps.ToList();
                ddlDepartment.DataBind();

                //add a default option to the dropdown
                ListItem default_item = new ListItem("-Select-", "0");
                ddlDepartment.Items.Insert(0, default_item);
            }
        }

        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create a new instructor
                Instructor objI = new Instructor();

                //fill the properties from the form inputs
                objI.FirstName = txtFirstName.Text;
                objI.LastName = txtLastName.Text;
                objI.Username = txtUsername.Text;
                objI.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);

                //salt and hash the plain text password
                String password = txtPassword.Text;
                String salt = CreateSalt(8);
                String pass_and_salt = password + salt;

                // Create a new instance of the hash crypto service provider.
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();

                // Convert the data to hash to an array of Bytes.
                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(pass_and_salt);

                // Compute the Hash. This returns an array of Bytes.
                byte[] bytHash = hashAlg.ComputeHash(bytValue);

                // Optionally, represent the hash value as a base64-encoded string,
                // For example, if you need to display the value or transmit it over a network.
                string base64 = Convert.ToBase64String(bytHash);

                objI.Password = base64;
                objI.Salt = salt;

                //save
                db.Instructors.Add(objI);
                db.SaveChanges();
            }
        }
    }
}