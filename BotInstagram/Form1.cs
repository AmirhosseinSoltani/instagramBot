using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotInstagram
{
    public partial class frmMain : Form
    {
        private  static UserSessionData user;
        

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            user = new UserSessionData();
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            ctx.api = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                .Build();

            var loginRequest = await ctx.api.LoginAsync();

            if (loginRequest.Succeeded)
            {
                gbLogin.Enabled = false;
                
                
            }
            else
            {
                MessageBox.Show("Login Failed .... \n "+loginRequest.Info.Message);
            }

        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            (new frmEdiotProfile()).ShowDialog();
        }

        

        

        private async void btnFollowers_Click(object sender, EventArgs e)
        {
            var followers = await ctx.api.UserProcessor.GetCurrentUserFollowersAsync(PaginationParameters.MaxPagesToLoad(1));
            foreach (var follower in followers.Value)
            {
                dgvFollowers.Rows.Add(follower.UserName, follower.FullName);
            }
           
        }

        private async void btnFollowing_Click(object sender, EventArgs e)
        {
            var currentusername =await ctx.api.UserProcessor.GetCurrentUserAsync();
            var follwings = await ctx.api.UserProcessor.GetUserFollowingAsync(currentusername.Value.UserName, PaginationParameters.MaxPagesToLoad(1));
            foreach (var follower in follwings.Value)
            {
                dgvFollowing.Rows.Add(follower.UserName, follower.FullName);
            }
        }

        private async void btnUserFollow_Click(object sender, EventArgs e)
        {
            var user = await ctx.api.UserProcessor.GetUserAsync(txtUser_UserName.Text);
            var follow =await ctx.api.UserProcessor.FollowUserAsync(user.Value.Pk);
            if (follow.Succeeded)
            {
                btnFollowing.PerformClick();
            }
        }

        private async void btnUnFollow_Click(object sender, EventArgs e)
        {
            var user = await ctx.api.UserProcessor.GetUserAsync(txtUser_UserName.Text);
            var unfollow = await ctx.api.UserProcessor.UnFollowUserAsync(user.Value.Pk);
            if (unfollow.Succeeded)
            {
                btnFollowing.PerformClick();
            }
        }

        private void DgvFollowing_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DgvFollowers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PcImage_Click(object sender, EventArgs e)
        {

        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void GbCommand_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
          
        }

        private void GbLogin_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text Files|*.txt";
            if (f.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(f.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        dataGridView1.Rows.Add(sr.ReadLine());
                    }
                }
            }
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    var list = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    var user = await ctx.api.UserProcessor.GetUserAsync(list);
                    var follow = await ctx.api.UserProcessor.FollowUserAsync(user.Value.Pk);
                    if (follow.Succeeded)
                    {
                        btnFollowing.PerformClick();
                    }
                    
                }
            }
            //var soton = dataGridView1.Rows[1].Cells
            //foreach (var follower in soton)
            //{
            //    var user = await ctx.api.UserProcessor.GetUserAsync();
            //    var follow = await ctx.api.UserProcessor.FollowUserAsync(user.Value.Pk);
            //    if (follow.Succeeded)
            //    {
            //        btnFollowing.PerformClick();
            //    }
            //}
           

        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    var list = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    var user = await ctx.api.UserProcessor.GetUserAsync(list);
                    var unfollow = await ctx.api.UserProcessor.UnFollowUserAsync(user.Value.Pk);
                    if (unfollow.Succeeded)
                    {
                        btnFollowing.PerformClick();
                    }

                }
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
