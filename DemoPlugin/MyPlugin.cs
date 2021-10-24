using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GOverlayPlugin.Interfaces;

namespace DemoPlugin
{
    public class MyPlugin : IPlugin
    {
        //Fields
        private IHost objHost;

        public string _pluginId;
        public string _pluginName;
        private string _plugindescription;
        private string _display = "lcdsys";

        //Properties
        public string Name => _pluginName;
        public string Description => _plugindescription;
        public string Display => _display;

        public MyPlugin(string id, string name, string description)
        {
            _pluginId = id;
            _pluginName = name;
            _plugindescription = description;
        }

        public void Initialize(IHost Host)
        {
            objHost = Host;
        }

        public Hashtable CallBacks(string method)
        {
            throw new NotImplementedException();
        }

        public bool SensorHasCustomDraw(string sensor_name)
        {
            throw new NotImplementedException();
        }

        public Hashtable ComboBoxes()
        {
            //Create custom ComboBox for your configuration to use
            var comboBoxes = new Hashtable();
            var myBox = new Hashtable();

            //Set each one of the Combobox options as value, Display Name
            myBox.Add("text1", "Display Hello World");
            myBox.Add("text2", "Display Bye World");

            //Add the combobox options as the combobox "DEMO.textSelector"
            comboBoxes.Add("DEMO.textSelector", myBox);

            return comboBoxes;
        }

        public Dictionary<string, string> AvailableSensors(Hashtable pluginOptions)
        {
            //Create the list of the sensors/ elements this plugin has
            //You can access your pluginOptions here as pluginOptions(your_option)

            //Options: SensorTag, Sensor Display-Name
            var sensors = new Dictionary<string, string>();

            string AddText = "";
            if (pluginOptions["appendyesno"].ToString() == "Yes")
            {
                AddText = pluginOptions["appendtext"].ToString();
            }

            sensors.Add("DEMO.helloworld", $"Hello World: {AddText}");

            return sensors;
        }

        public Hashtable PluginOptionsDefault()
        {
            //Set the default values you want to have on plugin, if the user doesnt change any option, he will have this settings
            var options = new Hashtable();

            options.Add("appendyesno", "No");
            options.Add("append", "");

            return options;
        }

        public Hashtable PluginOptions(Hashtable pluginData)
        {
            //Set the options the user will have when going to the plugins tab and clicking on your plugin
            //The availalbe option_type are the same as CreateOptions function
            var options = new Hashtable();

            //Option: option_index as integer, option_data as ArrayList
            //Option_Data: option_type as string, option_label as string, option_name as string (no spaces, no _)
            options.Add(0, new ArrayList()
            {
                "ComboYesNo",
                "Append Text",
                "appendyesno"
            });

            options.Add(1, new ArrayList()
            {
                "Text",
                "Append to the text",
                "append"
            });

            return options;
        }

        public Hashtable CreateOptions(string sensorId, Hashtable elementData)
        {
            //Set the options the user will have when clicking on the element


            var options = new Hashtable();
            if (sensorId == "DEMO.helloworld")
            {

                //Option: option_index as integer, option_data as ArrayList
                //Option_Data: option_type as string, option_label as string, option_name as string (no spaces, no _), help text
                options.Add(0,
                    new ArrayList() { "ColorBasic",
                        "Color of the Text",
                        "color",
                        "Pick a color for your text" });

                options.Add(1,
                    new ArrayList()
                    {
                        "DEMO.textSelector",
                        "Text to Display",
                        "textSelected",
                        "Do you want to display HelloWorld or ByeWorld?"
                    });
            }

            return options;
        }

        public Hashtable SetDefaultOptions(string sensorId, Hashtable elementData)
        {
            throw new NotImplementedException();
        }

        public ArrayList DisplayOnLCD(string sensorId, Hashtable elementData, Hashtable pluginOptions, int CacheRuns)
        {
            throw new NotImplementedException();
        }

        public ArrayList LCDSys2_DisplayOnLCD(string sensorId, Hashtable elementData, Hashtable pluginOptions, int CacheRuns)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> LCDSys2_AvailableSensors(Hashtable pluginOptions)
        {
            throw new NotImplementedException();
        }

        public Hashtable LCDSys2_CreateOptions(string sensorId, Hashtable elementData)
        {
            throw new NotImplementedException();
        }
    }
}
