using System;
using UIKit;
using System.Collections.Generic;
using Syncfusion.iOS.DataForm;

namespace DataFormTouchGesture
{

    public partial class ViewController : UIViewController
    {
        SfDataForm dataForm;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            dataForm = new SfDataForm(new CoreGraphics.CGRect(30, 0, this.View.Bounds.Width, this.View.Bounds.Height))
            {
                DataObject =
                new Employees()
                {
                    ContactID = 1001,
                    EmployeeID = 1709,
                    Title = "Software"
                },
                BackgroundColor = UIColor.White
            };

            dataForm.IsReadOnly = true;
            dataForm.LayoutManager = new DataFormLayoutManagerExt(dataForm);
            dataForm.UserInteractionEnabled = true;
            this.View = dataForm;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
    public class DataFormLayoutManagerExt : DataFormLayoutManager
    {
        public DataFormLayoutManagerExt(SfDataForm dataForm) : base(dataForm)
        {

        }

        protected override DataFormItemView CreateDataFormItemView(int rowIndex, int columnIndex)
        {
            var view = base.CreateDataFormItemView(rowIndex, columnIndex);
            view.UserInteractionEnabled = true;
            // Gesture added for DataformItemView
            UITapGestureRecognizer gestureRecognizers = new UITapGestureRecognizer();
            gestureRecognizers.NumberOfTapsRequired = 1;
            if (view != null)
            {                
                view.UserInteractionEnabled = true;
                gestureRecognizers.AddTarget(() =>
                {
                    // event raise for DataFormItemView
                    OnClickEvent(gestureRecognizers);
                });
                view.AddGestureRecognizer(gestureRecognizers);
            }
            return view;
        }

        private void OnClickEvent(UITapGestureRecognizer gestureRecognizer)
        {
            var dataFormItemView = gestureRecognizer.View;
            var touchPosition = gestureRecognizer.LocationInView(dataFormItemView);


            var labelFrame = ((dataFormItemView as DataFormItemView).Subviews[0] as UIView).Frame;

            var editorFrame = ((dataFormItemView as DataFormItemView).EditorView as UIView).Frame;

            // Editor element has been tapped whether the below condition is true
            if (editorFrame.Contains(touchPosition.X, touchPosition.Y))
            {
                // your code here
            }
            // Label/Image has been tapped whether the below condition is true.
            else if (labelFrame.Contains(touchPosition.X, touchPosition.Y))
            {
                // your code here
            }
        }
    }
}

