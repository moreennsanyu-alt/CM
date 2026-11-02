using System;
using System.Windows.Forms;

namespace ClinicManager.Win;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "ClinicManager";
        Width = 1024;
        Height = 768;
        StartPosition = FormStartPosition.CenterScreen;
    }
}
