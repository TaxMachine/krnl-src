

// SirhurtUI.Controls.TabPanelControl
using System;
using System.Windows.Forms;

public class TabPanelControl : UserControl
{
	public TabPanelControl()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		SuspendLayout();
		base.Name = "TabPanelControl";
		base.Load += new System.EventHandler(TabPanelControl_Load);
		ResumeLayout(false);
	}

	private void TabPanelControl_Load(object sender, EventArgs e)
	{
	}
}
