using System;
using Gtk;
using Mono.Unix;

public partial class MainWindow: Gtk.Window
{	
	public enum OperationType: uint {
		DEFAULT,
		ADD,
		SUB,
		MUL,
		DIV
	}

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButtonQuitClicked (object sender, System.EventArgs e)
	{
		Application.Quit ();
	}

	protected void ShowError (String errorMessage) {
		MessageDialog md = new MessageDialog (this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, errorMessage);
		md.Run ();
		md.Destroy ();
		return;
	}

	protected void OnButtonOperationClicked (object sender, System.EventArgs e)
	{
		String buttonName = ((Gtk.Button)sender).Name;
		OperationType currentOperation = OperationType.DEFAULT;

		if (buttonName.Equals ("buttonAdd")) {
			currentOperation = OperationType.ADD;
		} else if (buttonName.Equals ("buttonSub")) {
			currentOperation = OperationType.SUB;
		} else if (buttonName.Equals ("buttonMul")) {
			currentOperation = OperationType.MUL;
		} else if (buttonName.Equals ("buttonDiv")) {
			currentOperation = OperationType.DIV;
		}

		if (entryFirst.Text.Length == 0 || entrySecond.Text.Length == 0) {
			ShowError (Catalog.GetString ("First and second argument must be filled."));
			return;
		}

		Double opFirst = 0, opSecond = 0, resultValue = 0;

		try {
			opFirst = Convert.ToDouble(entryFirst.Text);
			opSecond = Convert.ToDouble(entrySecond.Text);
		} catch (System.FormatException) {
			ShowError (Catalog.GetString ("First and second argument must be floating-point numbers."));
			return;
		}

		switch (currentOperation) {
		case OperationType.ADD:
			labelOperation.Text = Catalog.GetString ("Addition");
			resultValue = opFirst + opSecond;
			break;
		case OperationType.SUB:
			labelOperation.Text = Catalog.GetString ("Subtraction");
			resultValue = opFirst - opSecond;
			break;
		case OperationType.MUL:
			labelOperation.Text = Catalog.GetString ("Multiplication");
			resultValue = opFirst * opSecond;
			break;
		case OperationType.DIV:
			labelOperation.Text = Catalog.GetString ("Division");
			if (opSecond == 0) {
				ShowError (Catalog.GetString ("You can not divide by zero!"));
				entryResult.Text = String.Empty;
				return;
			}
			resultValue = opFirst / opSecond;
			break;
		default:
			return;
		}

		entryResult.Text = Convert.ToString (resultValue);

	}

	protected void OnButtonRstClicked (object sender, System.EventArgs e)
	{
		labelOperation.Text = Catalog.GetString ("Choose operation");
		entryFirst.Text = String.Empty;
		entrySecond.Text = String.Empty;
		entryResult.Text = String.Empty;
	}
}
