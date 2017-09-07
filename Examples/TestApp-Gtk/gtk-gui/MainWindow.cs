
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;

	private global::Gtk.Action ArchivoAction;

	private global::Gtk.Action PruebasAction;

	private global::Gtk.Action CapturaDePantallaAction;

	private global::Gtk.Action Action;

	private global::Gtk.Action SalirAction;

	private global::Gtk.Action ContraseasAction;

	private global::Gtk.Action ProbarContraseaAction;

	private global::Gtk.Action EstablecerContraseaAction;

	private global::Gtk.Action GenerarContraseaAction;

	private global::Gtk.Action PinAction;

	private global::Gtk.Action ContraseaSencillaAction;

	private global::Gtk.Action ContraseaComplejaAction;

	private global::Gtk.Action PluginsAction;

	private global::Gtk.Action AyudaAction;

	private global::Gtk.Action InfoDePluginsAction1;

	private global::Gtk.Action AcercaDeMCARTAction;

	private global::Gtk.VBox vbox1;

	private global::Gtk.MenuBar mnuMain;

	private global::Gtk.Image image1;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup("Default");
		this.ArchivoAction = new global::Gtk.Action("ArchivoAction", global::Mono.Unix.Catalog.GetString("Archivo"), null, null);
		this.ArchivoAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Archivo");
		w1.Add(this.ArchivoAction, null);
		this.PruebasAction = new global::Gtk.Action("PruebasAction", global::Mono.Unix.Catalog.GetString("Pruebas"), null, null);
		this.PruebasAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Pruebas");
		w1.Add(this.PruebasAction, null);
		this.CapturaDePantallaAction = new global::Gtk.Action("CapturaDePantallaAction", global::Mono.Unix.Catalog.GetString("Captura de pantalla"), null, null);
		this.CapturaDePantallaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Captura de pantalla");
		w1.Add(this.CapturaDePantallaAction, null);
		this.Action = new global::Gtk.Action("Action", global::Mono.Unix.Catalog.GetString("-"), null, null);
		this.Action.ShortLabel = global::Mono.Unix.Catalog.GetString("-");
		w1.Add(this.Action, null);
		this.SalirAction = new global::Gtk.Action("SalirAction", global::Mono.Unix.Catalog.GetString("Salir"), null, null);
		this.SalirAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Salir");
		w1.Add(this.SalirAction, null);
		this.ContraseasAction = new global::Gtk.Action("ContraseasAction", global::Mono.Unix.Catalog.GetString("Contraseñas"), null, null);
		this.ContraseasAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Contraseñas");
		w1.Add(this.ContraseasAction, null);
		this.ProbarContraseaAction = new global::Gtk.Action("ProbarContraseaAction", global::Mono.Unix.Catalog.GetString("Probar contraseña..."), null, null);
		this.ProbarContraseaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Probar contraseña...");
		w1.Add(this.ProbarContraseaAction, null);
		this.EstablecerContraseaAction = new global::Gtk.Action("EstablecerContraseaAction", global::Mono.Unix.Catalog.GetString("Establecer contraseña..."), null, null);
		this.EstablecerContraseaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Establecer contraseña...");
		w1.Add(this.EstablecerContraseaAction, null);
		this.GenerarContraseaAction = new global::Gtk.Action("GenerarContraseaAction", global::Mono.Unix.Catalog.GetString("Generar contraseña"), null, null);
		this.GenerarContraseaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Generar contraseña");
		w1.Add(this.GenerarContraseaAction, null);
		this.PinAction = new global::Gtk.Action("PinAction", global::Mono.Unix.Catalog.GetString("Pin..."), null, null);
		this.PinAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Pin...");
		w1.Add(this.PinAction, null);
		this.ContraseaSencillaAction = new global::Gtk.Action("ContraseaSencillaAction", global::Mono.Unix.Catalog.GetString("Contraseña sencilla"), null, null);
		this.ContraseaSencillaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Contraseña sencilla");
		w1.Add(this.ContraseaSencillaAction, null);
		this.ContraseaComplejaAction = new global::Gtk.Action("ContraseaComplejaAction", global::Mono.Unix.Catalog.GetString("Contraseña compleja"), null, null);
		this.ContraseaComplejaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Contraseña compleja");
		w1.Add(this.ContraseaComplejaAction, null);
		this.PluginsAction = new global::Gtk.Action("PluginsAction", global::Mono.Unix.Catalog.GetString("Plugins"), null, null);
		this.PluginsAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Plugins");
		w1.Add(this.PluginsAction, null);
		this.AyudaAction = new global::Gtk.Action("AyudaAction", global::Mono.Unix.Catalog.GetString("Ayuda"), null, null);
		this.AyudaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Ayuda");
		w1.Add(this.AyudaAction, null);
		this.InfoDePluginsAction1 = new global::Gtk.Action("InfoDePluginsAction1", global::Mono.Unix.Catalog.GetString("Info de plugins..."), null, null);
		this.InfoDePluginsAction1.ShortLabel = global::Mono.Unix.Catalog.GetString("Info de plugins...");
		w1.Add(this.InfoDePluginsAction1, null);
		this.AcercaDeMCARTAction = new global::Gtk.Action("AcercaDeMCARTAction", global::Mono.Unix.Catalog.GetString("Acerca de MCART..."), null, null);
		this.AcercaDeMCARTAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Acerca de MCART...");
		w1.Add(this.AcercaDeMCARTAction, null);
		this.UIManager.InsertActionGroup(w1, 0);
		this.AddAccelGroup(this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString(@"<ui><menubar name='mnuMain'><menu name='ArchivoAction' action='ArchivoAction'><menuitem name='CapturaDePantallaAction' action='CapturaDePantallaAction'/><separator/><menuitem name='SalirAction' action='SalirAction'/></menu><menu name='PruebasAction' action='PruebasAction'><menu name='ContraseasAction' action='ContraseasAction'><menuitem name='ProbarContraseaAction' action='ProbarContraseaAction'/><menuitem name='EstablecerContraseaAction' action='EstablecerContraseaAction'/><menu name='GenerarContraseaAction' action='GenerarContraseaAction'><menuitem name='PinAction' action='PinAction'/><menuitem name='ContraseaSencillaAction' action='ContraseaSencillaAction'/><menuitem name='ContraseaComplejaAction' action='ContraseaComplejaAction'/></menu></menu></menu><menu name='PluginsAction' action='PluginsAction'/><menu name='AyudaAction' action='AyudaAction'><menuitem name='InfoDePluginsAction1' action='InfoDePluginsAction1'/><menuitem name='AcercaDeMCARTAction' action='AcercaDeMCARTAction'/></menu></menubar></ui>");
		this.mnuMain = ((global::Gtk.MenuBar)(this.UIManager.GetWidget("/mnuMain")));
		this.mnuMain.Name = "mnuMain";
		this.vbox1.Add(this.mnuMain);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.mnuMain]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.image1 = new global::Gtk.Image();
		this.image1.Name = "image1";
		this.vbox1.Add(this.image1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.image1]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		this.Add(this.vbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 466;
		this.DefaultHeight = 300;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.SalirAction.Activated += new global::System.EventHandler(this.OnSalirActionActivated);
		this.InfoDePluginsAction1.Activated += new global::System.EventHandler(this.OnInfoDePluginsAction1Activated);
	}
}
