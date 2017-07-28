﻿using gEngine.Commands;
using gEngine.Data.Interface;
using gEngine.Graph.Interface;
using gEngine.Project.Controls;
using gEngine.Project.Ge.Plane.Controls;
using gEngine.Util.Ge.Plane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace gEngine.Project.Ge.Plane.Commands
{
    public class NewPlaneLineCommand: CommandBinding
    {
        public NewPlaneLineCommand()
        {
            Command = PlaneCommands.NewPlaneLineCommand;
            Executed += NewPlaneLineCommand_Executed;
            CanExecute += NewPlaneLineCommand_CanExecute;
        }

        private void NewPlaneLineCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ProjectControl pc = e.OriginalSource as ProjectControl;
            if (pc == null || pc.Project == null || pc.Project.DBSource == null)
                return;


            MapsControl tc = pc.MapsControl;
           

            //List<string> names = pc.Project.DBSource.WellLocationsNames;
            //if (names.Count == 0)
            //    return;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void NewPlaneLineCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectControl pctrl = e.OriginalSource as ProjectControl;
            if (pctrl == null || pctrl.Project == null || pctrl.Project.DBSource == null)
                return;

            List<string> names = pctrl.Project.DBSource.WellLocationsNames;
            if (names.Count == 0)
                return;

            //弹出对话框，选择信息？
            SelectLineStyleLocationDocu msg = new SelectLineStyleLocationDocu(null);
            msg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            msg.Title = "线型";

            if (msg.ShowDialog() == true)
            {
                
                IDBWellLocations wls = pctrl.Project.DBSource.GetWellLocations(msg.FileName);
                PlaneLayerCreator pc = new PlaneLayerCreator();
                gEngine.Graph.Ge.Layer layer = pc.CreateWellLocationLayer(wls);

                //先增加layer，再创建IMap
                layer.Name = "平面图Layer图层";
                layer.Visible = true;
                layer.Editable = true;
                ILayers layers = new ILayers();
                layers.Add(layer);
                IMap map = pctrl.Project.NewMap("Ge", msg.MapName, layers);
            }
            e.Handled = true;
        }

    }
}