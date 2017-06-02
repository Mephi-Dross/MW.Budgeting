namespace MW.Budgeting.UI.Accounts
{
    partial class AccountScreen
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EntrySplitter = new System.Windows.Forms.SplitContainer();
            this.tlpEntries = new System.Windows.Forms.TableLayoutPanel();
            this.dgEntries = new System.Windows.Forms.DataGridView();
            this.tlpAutoEntries = new System.Windows.Forms.TableLayoutPanel();
            this.dgAutoEntries = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.EntrySplitter)).BeginInit();
            this.EntrySplitter.Panel1.SuspendLayout();
            this.EntrySplitter.Panel2.SuspendLayout();
            this.EntrySplitter.SuspendLayout();
            this.tlpEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEntries)).BeginInit();
            this.tlpAutoEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAutoEntries)).BeginInit();
            this.SuspendLayout();
            // 
            // EntrySplitter
            // 
            this.EntrySplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntrySplitter.Location = new System.Drawing.Point(0, 0);
            this.EntrySplitter.Name = "EntrySplitter";
            this.EntrySplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // EntrySplitter.Panel1
            // 
            this.EntrySplitter.Panel1.Controls.Add(this.tlpEntries);
            this.EntrySplitter.Panel1MinSize = 75;
            // 
            // EntrySplitter.Panel2
            // 
            this.EntrySplitter.Panel2.Controls.Add(this.tlpAutoEntries);
            this.EntrySplitter.Panel2MinSize = 75;
            this.EntrySplitter.Size = new System.Drawing.Size(640, 480);
            this.EntrySplitter.SplitterDistance = 380;
            this.EntrySplitter.TabIndex = 0;
            // 
            // tlpEntries
            // 
            this.tlpEntries.ColumnCount = 1;
            this.tlpEntries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEntries.Controls.Add(this.dgEntries, 0, 0);
            this.tlpEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEntries.Location = new System.Drawing.Point(0, 0);
            this.tlpEntries.Name = "tlpEntries";
            this.tlpEntries.Padding = new System.Windows.Forms.Padding(5);
            this.tlpEntries.RowCount = 1;
            this.tlpEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEntries.Size = new System.Drawing.Size(640, 380);
            this.tlpEntries.TabIndex = 1;
            // 
            // dgEntries
            // 
            this.dgEntries.AllowUserToOrderColumns = true;
            this.dgEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgEntries.Location = new System.Drawing.Point(8, 8);
            this.dgEntries.Name = "dgEntries";
            this.dgEntries.Size = new System.Drawing.Size(624, 364);
            this.dgEntries.TabIndex = 0;
            // 
            // tlpAutoEntries
            // 
            this.tlpAutoEntries.ColumnCount = 1;
            this.tlpAutoEntries.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAutoEntries.Controls.Add(this.dgAutoEntries, 0, 0);
            this.tlpAutoEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAutoEntries.Location = new System.Drawing.Point(0, 0);
            this.tlpAutoEntries.Name = "tlpAutoEntries";
            this.tlpAutoEntries.Padding = new System.Windows.Forms.Padding(5);
            this.tlpAutoEntries.RowCount = 1;
            this.tlpAutoEntries.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpAutoEntries.Size = new System.Drawing.Size(640, 96);
            this.tlpAutoEntries.TabIndex = 1;
            // 
            // dgAutoEntries
            // 
            this.dgAutoEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgAutoEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAutoEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAutoEntries.Location = new System.Drawing.Point(8, 8);
            this.dgAutoEntries.Name = "dgAutoEntries";
            this.dgAutoEntries.Size = new System.Drawing.Size(624, 80);
            this.dgAutoEntries.TabIndex = 0;
            // 
            // AccountScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EntrySplitter);
            this.Name = "AccountScreen";
            this.Size = new System.Drawing.Size(640, 480);
            this.EntrySplitter.Panel1.ResumeLayout(false);
            this.EntrySplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EntrySplitter)).EndInit();
            this.EntrySplitter.ResumeLayout(false);
            this.tlpEntries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEntries)).EndInit();
            this.tlpAutoEntries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAutoEntries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer EntrySplitter;
        private System.Windows.Forms.DataGridView dgEntries;
        private System.Windows.Forms.DataGridView dgAutoEntries;
        private System.Windows.Forms.TableLayoutPanel tlpEntries;
        private System.Windows.Forms.TableLayoutPanel tlpAutoEntries;
    }
}
