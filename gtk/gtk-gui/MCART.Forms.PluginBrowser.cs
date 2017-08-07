
// This file has been generated by the GUI designer. Do not modify.
namespace MCART.Forms
{
	public partial class PluginBrowser
	{
		private global::Gtk.UIManager UIManager;

		private global::Gtk.HPaned hpaned1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView trvPlugins;

		private global::Gtk.Table table3;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Entry txtVer;

		private global::Gtk.CheckButton chkIsBeta;

		private global::Gtk.CheckButton chkIsUnafe;

		private global::Gtk.Label label6;

		private global::Gtk.Label label7;

		private global::Gtk.Label label9;

		private global::Gtk.Notebook notebook1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TextView txtLicense;

		private global::Gtk.Label label1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow2;

		private global::Gtk.TreeView trvInterfaces;

		private global::Gtk.Label label2;

		private global::Gtk.Table table1;

		private global::Gtk.CheckButton chkMinVer;

		private global::Gtk.Entry txtMinVer;

		private global::Gtk.CheckButton chkTgtVer;

		private global::Gtk.Entry txtTgtVer;

		private global::Gtk.Label label10;

		private global::Gtk.Label label5;

		private global::Gtk.Label lblVeredict;

		private global::Gtk.Label label3;

		private global::Gtk.MenuBar mnuInteractions;

		private global::Gtk.Label label4;

		private global::Gtk.Entry txtCopyright;

		private global::Gtk.Entry txtName;

		private global::Gtk.VBox vbox4;

		private global::Gtk.Label label8;

		private global::Gtk.Entry txtDesc;

		private global::Gtk.Button btnClose;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget MCART.Forms.PluginBrowser
			this.UIManager = new global::Gtk.UIManager();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup("Default");
			this.UIManager.InsertActionGroup(w1, 0);
			this.AddAccelGroup(this.UIManager.AccelGroup);
			this.Name = "MCART.Forms.PluginBrowser";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child MCART.Forms.PluginBrowser.VBox
			global::Gtk.VBox w2 = this.VBox;
			w2.Name = "dialog1_VBox";
			w2.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hpaned1 = new global::Gtk.HPaned();
			this.hpaned1.CanFocus = true;
			this.hpaned1.Name = "hpaned1";
			this.hpaned1.Position = 246;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.trvPlugins = new global::Gtk.TreeView();
			this.trvPlugins.CanFocus = true;
			this.trvPlugins.Name = "trvPlugins";
			this.GtkScrolledWindow.Add(this.trvPlugins);
			this.hpaned1.Add(this.GtkScrolledWindow);
			global::Gtk.Paned.PanedChild w4 = ((global::Gtk.Paned.PanedChild)(this.hpaned1[this.GtkScrolledWindow]));
			w4.Resize = false;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.table3 = new global::Gtk.Table(((uint)(5)), ((uint)(2)), false);
			this.table3.Name = "table3";
			this.table3.RowSpacing = ((uint)(6));
			this.table3.ColumnSpacing = ((uint)(6));
			// Container child table3.Gtk.Table+TableChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.txtVer = new global::Gtk.Entry();
			this.txtVer.CanFocus = true;
			this.txtVer.Name = "txtVer";
			this.txtVer.IsEditable = false;
			this.txtVer.InvisibleChar = '●';
			this.hbox2.Add(this.txtVer);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.txtVer]));
			w5.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.chkIsBeta = new global::Gtk.CheckButton();
			this.chkIsBeta.Sensitive = false;
			this.chkIsBeta.CanFocus = true;
			this.chkIsBeta.Name = "chkIsBeta";
			this.chkIsBeta.Label = global::Mono.Unix.Catalog.GetString("Beta");
			this.chkIsBeta.DrawIndicator = true;
			this.chkIsBeta.UseUnderline = true;
			this.hbox2.Add(this.chkIsBeta);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.chkIsBeta]));
			w6.Position = 1;
			w6.Expand = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.chkIsUnafe = new global::Gtk.CheckButton();
			this.chkIsUnafe.Sensitive = false;
			this.chkIsUnafe.CanFocus = true;
			this.chkIsUnafe.Name = "chkIsUnafe";
			this.chkIsUnafe.Label = global::Mono.Unix.Catalog.GetString("Inestable");
			this.chkIsUnafe.DrawIndicator = true;
			this.chkIsUnafe.UseUnderline = true;
			this.hbox2.Add(this.chkIsUnafe);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.chkIsUnafe]));
			w7.Position = 2;
			w7.Expand = false;
			this.table3.Add(this.hbox2);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table3[this.hbox2]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Nombre");
			this.table3.Add(this.label6);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table3[this.label6]));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Versión");
			this.table3.Add(this.label7);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table3[this.label7]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label();
			this.label9.Name = "label9";
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("Copyright");
			this.table3.Add(this.label9);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table3[this.label9]));
			w11.TopAttach = ((uint)(3));
			w11.BottomAttach = ((uint)(4));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.notebook1 = new global::Gtk.Notebook();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 3;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.txtLicense = new global::Gtk.TextView();
			this.txtLicense.CanFocus = true;
			this.txtLicense.Name = "txtLicense";
			this.GtkScrolledWindow1.Add(this.txtLicense);
			this.notebook1.Add(this.GtkScrolledWindow1);
			// Notebook tab
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Licencia");
			this.notebook1.SetTabLabel(this.GtkScrolledWindow1, this.label1);
			this.label1.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.trvInterfaces = new global::Gtk.TreeView();
			this.trvInterfaces.CanFocus = true;
			this.trvInterfaces.Name = "trvInterfaces";
			this.GtkScrolledWindow2.Add(this.trvInterfaces);
			this.notebook1.Add(this.GtkScrolledWindow2);
			global::Gtk.Notebook.NotebookChild w15 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.GtkScrolledWindow2]));
			w15.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Interfaces");
			this.notebook1.SetTabLabel(this.GtkScrolledWindow2, this.label2);
			this.label2.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.table1 = new global::Gtk.Table(((uint)(3)), ((uint)(2)), true);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.chkMinVer = new global::Gtk.CheckButton();
			this.chkMinVer.Sensitive = false;
			this.chkMinVer.CanFocus = true;
			this.chkMinVer.Name = "chkMinVer";
			this.chkMinVer.Label = global::Mono.Unix.Catalog.GetString("checkbutton1");
			this.chkMinVer.DrawIndicator = true;
			this.chkMinVer.UseUnderline = true;
			this.chkMinVer.Remove(this.chkMinVer.Child);
			// Container child chkMinVer.Gtk.Container+ContainerChild
			this.txtMinVer = new global::Gtk.Entry();
			this.txtMinVer.CanFocus = true;
			this.txtMinVer.Name = "txtMinVer";
			this.txtMinVer.IsEditable = true;
			this.txtMinVer.InvisibleChar = '●';
			this.chkMinVer.Add(this.txtMinVer);
			this.table1.Add(this.chkMinVer);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1[this.chkMinVer]));
			w17.LeftAttach = ((uint)(1));
			w17.RightAttach = ((uint)(2));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.chkTgtVer = new global::Gtk.CheckButton();
			this.chkTgtVer.Sensitive = false;
			this.chkTgtVer.CanFocus = true;
			this.chkTgtVer.Name = "chkTgtVer";
			this.chkTgtVer.Label = global::Mono.Unix.Catalog.GetString("checkbutton2");
			this.chkTgtVer.DrawIndicator = true;
			this.chkTgtVer.UseUnderline = true;
			this.chkTgtVer.Remove(this.chkTgtVer.Child);
			// Container child chkTgtVer.Gtk.Container+ContainerChild
			this.txtTgtVer = new global::Gtk.Entry();
			this.txtTgtVer.CanFocus = true;
			this.txtTgtVer.Name = "txtTgtVer";
			this.txtTgtVer.IsEditable = true;
			this.txtTgtVer.InvisibleChar = '●';
			this.chkTgtVer.Add(this.txtTgtVer);
			this.table1.Add(this.chkTgtVer);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1[this.chkTgtVer]));
			w19.TopAttach = ((uint)(1));
			w19.BottomAttach = ((uint)(2));
			w19.LeftAttach = ((uint)(1));
			w19.RightAttach = ((uint)(2));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label10 = new global::Gtk.Label();
			this.label10.Name = "label10";
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString("Versión objetivo de MCART");
			this.table1.Add(this.label10);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1[this.label10]));
			w20.TopAttach = ((uint)(1));
			w20.BottomAttach = ((uint)(2));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Versión mínima de MCART");
			this.table1.Add(this.label5);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1[this.label5]));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblVeredict = new global::Gtk.Label();
			this.lblVeredict.Name = "lblVeredict";
			this.lblVeredict.LabelProp = global::Mono.Unix.Catalog.GetString("Seleccione un plugin para comprobar la compatibilidad.");
			this.table1.Add(this.lblVeredict);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1[this.lblVeredict]));
			w22.TopAttach = ((uint)(2));
			w22.BottomAttach = ((uint)(3));
			w22.RightAttach = ((uint)(2));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			this.notebook1.Add(this.table1);
			global::Gtk.Notebook.NotebookChild w23 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.table1]));
			w23.Position = 2;
			// Notebook tab
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Compatibilidad");
			this.notebook1.SetTabLabel(this.table1, this.label3);
			this.label3.ShowAll();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.UIManager.AddUiFromString("<ui><menubar name='mnuInteractions'/></ui>");
			this.mnuInteractions = ((global::Gtk.MenuBar)(this.UIManager.GetWidget("/mnuInteractions")));
			this.mnuInteractions.Name = "mnuInteractions";
			this.notebook1.Add(this.mnuInteractions);
			global::Gtk.Notebook.NotebookChild w24 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.mnuInteractions]));
			w24.Position = 3;
			// Notebook tab
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Interacciones");
			this.notebook1.SetTabLabel(this.mnuInteractions, this.label4);
			this.label4.ShowAll();
			this.table3.Add(this.notebook1);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table3[this.notebook1]));
			w25.TopAttach = ((uint)(4));
			w25.BottomAttach = ((uint)(5));
			w25.RightAttach = ((uint)(2));
			// Container child table3.Gtk.Table+TableChild
			this.txtCopyright = new global::Gtk.Entry();
			this.txtCopyright.CanFocus = true;
			this.txtCopyright.Name = "txtCopyright";
			this.txtCopyright.IsEditable = false;
			this.txtCopyright.InvisibleChar = '●';
			this.table3.Add(this.txtCopyright);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table3[this.txtCopyright]));
			w26.TopAttach = ((uint)(3));
			w26.BottomAttach = ((uint)(4));
			w26.LeftAttach = ((uint)(1));
			w26.RightAttach = ((uint)(2));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.txtName = new global::Gtk.Entry();
			this.txtName.CanFocus = true;
			this.txtName.Name = "txtName";
			this.txtName.IsEditable = false;
			this.txtName.InvisibleChar = '●';
			this.table3.Add(this.txtName);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table3[this.txtName]));
			w27.LeftAttach = ((uint)(1));
			w27.RightAttach = ((uint)(2));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table3.Gtk.Table+TableChild
			this.vbox4 = new global::Gtk.VBox();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.label8 = new global::Gtk.Label();
			this.label8.Name = "label8";
			this.label8.Xalign = 0F;
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString("Descripción");
			this.vbox4.Add(this.label8);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.label8]));
			w28.Position = 0;
			w28.Expand = false;
			w28.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.txtDesc = new global::Gtk.Entry();
			this.txtDesc.CanFocus = true;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.IsEditable = true;
			this.txtDesc.InvisibleChar = '●';
			this.vbox4.Add(this.txtDesc);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.txtDesc]));
			w29.Position = 1;
			w29.Expand = false;
			w29.Fill = false;
			this.table3.Add(this.vbox4);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table3[this.vbox4]));
			w30.TopAttach = ((uint)(2));
			w30.BottomAttach = ((uint)(3));
			w30.RightAttach = ((uint)(2));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hpaned1.Add(this.table3);
			w2.Add(this.hpaned1);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(w2[this.hpaned1]));
			w32.Position = 0;
			// Internal child MCART.Forms.PluginBrowser.ActionArea
			global::Gtk.HButtonBox w33 = this.ActionArea;
			w33.Name = "dialog1_ActionArea";
			w33.Spacing = 10;
			w33.BorderWidth = ((uint)(5));
			w33.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.btnClose = new global::Gtk.Button();
			this.btnClose.CanDefault = true;
			this.btnClose.CanFocus = true;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseStock = true;
			this.btnClose.UseUnderline = true;
			this.btnClose.Label = "gtk-close";
			this.AddActionWidget(this.btnClose, -7);
			global::Gtk.ButtonBox.ButtonBoxChild w34 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w33[this.btnClose]));
			w34.Expand = false;
			w34.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 653;
			this.DefaultHeight = 418;
			this.Show();
			this.trvPlugins.CursorChanged += new global::System.EventHandler(this.OnTrvPluginsCursorChanged);
			this.btnClose.Clicked += new global::System.EventHandler(this.BtnClose_Click);
		}
	}
}
