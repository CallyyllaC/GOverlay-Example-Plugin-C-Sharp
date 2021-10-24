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

        public MyPlugin()
        {
            _pluginId = "11523579";
            _pluginName = "CSharp Plugin";
            _plugindescription = "Example Plugin in CSharp";
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
            //Set the default values you want to have on plugin
            //if the user doesnt change any option, he will have this settings
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
            //Option_Data: option_type as string, option_label as string
            //option_name as string (no spaces, no _)
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
                //Option_Data: option_type as string, option_label as string
                //option_name as string (no spaces, no _), help text
                options.Add(0,
                    new ArrayList() { "ColorBasic",
                        "Colour of the Text",
                        "color",
                        "Pick a colour for your text" });

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
            //Set the default values you want to have on your sensor when its created
            //if the user doesnt change any option, he will have this settings

            if (sensorId == "DEMO.helloworld")
            {
                elementData["width"] = 100;
                elementData["height"] = 41;
                elementData["color"] = 0;
                elementData["textSelected"] = "text1";
            }
            else
            {
                //There must be at least one width and height set
                //otherwise the element wont show on the display - emulator window because it has no size
                elementData["width"] = 50;
                elementData["height"] = 50;
            }

            return elementData;
        }

        public ArrayList DisplayOnLCD(string sensorId, Hashtable elementData, Hashtable pluginOptions, int CacheRuns)
        {
            var commandList = new ArrayList();

            //Draw on the screen
            if (sensorId == "DEMO.helloworld")
            {
                var x = elementData["x"];    //grab X position of the element
                var y = elementData["y"];    //grab Y position of the element

                var textToDraw = "";

                if (elementData["textSelected"] == "text1")
                {
                    textToDraw = "Hello World";
                }
                else
                {
                    textToDraw = "Bye World";
                }


                if (pluginOptions["appendyesno"] == "Yes")
                {
                    textToDraw = $"{textToDraw}{pluginOptions["append"]}";
                }

                //Draw Text (command as string, x as integer, y as integer, text as string, reserve_width as integer, unused as bool, unused as bool, unused as integer, basic_color as integer
                commandList.Add(new ArrayList()
                    { "text",
                        x,
                        y,
                        textToDraw,
                        0,
                        false,
                        false,
                        0,
                        elementData["color"]
                    });
            }

            return commandList;

        }

        public ArrayList LCDSys2_DisplayOnLCD(string sensorId, Hashtable elementData, Hashtable pluginOptions, int CacheRuns)
        {
            return DisplayOnLCD(sensorId, elementData, pluginOptions, CacheRuns);
        }

        public Dictionary<string, string> LCDSys2_AvailableSensors(Hashtable pluginOptions)
        {
            return AvailableSensors(pluginOptions);
        }

        public Hashtable LCDSys2_CreateOptions(string sensorId, Hashtable elementData)
        {
            return CreateOptions(sensorId, elementData);
        }
    }
}
