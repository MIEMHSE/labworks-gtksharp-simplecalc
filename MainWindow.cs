using System;
using Gtk;

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

		if (entryFirst.Text.Equals ("") || entrySecond.Text.Equals ("")) {
			ShowError ("Первый и второй аргумент должны быть заполнены!");
			return;
		}

		Double opFirst = 0, opSecond = 0, resultValue = 0;

		try {
			opFirst = Convert.ToDouble(entryFirst.Text);
			opSecond = Convert.ToDouble(entrySecond.Text);
		} catch (System.FormatException) {
			ShowError ("Первый и второй аргумент должны быть числами с плавающей запятой!");
			return;
		}

		switch (currentOperation) {
		case OperationType.ADD:
			labelOperation.Text = "Сложение";
			resultValue = opFirst + opSecond;
			break;
		case OperationType.SUB:
			labelOperation.Text = "Вычитание";
			resultValue = opFirst - opSecond;
			break;
		case OperationType.MUL:
			labelOperation.Text = "Умножение";
			resultValue = opFirst * opSecond;
			break;
		case OperationType.DIV:
			labelOperation.Text = "Деление";
			if (opSecond == 0) {
				ShowError ("Делить на ноль нельзя!");
				entryResult.Text = "";
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
		labelOperation.Text = "Выберите операцию";
		entryFirst.Text = "";
		entrySecond.Text = "";
		entryResult.Text = "";
	}
}
