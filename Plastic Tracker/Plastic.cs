using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Plastic_Tracker
{
    public static class PlasticManager
    {
        // Objects & Variables
        private static string saveFile = AppDomain.CurrentDomain.BaseDirectory + "Plastics.json";
        private static Dictionary<string, Plastic> plastics = new Dictionary<string, Plastic>();

        // Public Functions

        public static void addNewPlastic(Plastic plastic) {
            if (plastics.ContainsKey(plastic.name)) {
                MessageBox.Show($"You already have a plastic entered for '{plastic.name}"); ;
                return;
            }

            plastics.Add(plastic.name, plastic);
            save();
        }

        public static void deletePlastic(string name) {
            if (plastics.ContainsKey(name)) {
                plastics.Remove(name);
            }
            else {
                MessageBox.Show($"Could not delete unknown plastic: '{name}'");
            }
        }

        public static List<Plastic> getAllPlastics() {
            return plastics.Values.OrderBy(plastic => plastic.name).ToList();
        }

        public static void removeAmountOfPlastic(string name, int amount) {
            plastics[name].remaining -= amount;
            if(plastics[name].remaining <= 0) {
                deletePlastic(name);
            }
        }

        public static void removeAmountOfPlastic(Plastic plastic, int amount) {
            removeAmountOfPlastic(plastic.name, amount);
        }

        public static void setAmountOfPlastic(string name, int amount) {
            plastics[name].remaining = amount;
        }

        public static void setAmountOfPlastic(Plastic plastic, int amount) {
            setAmountOfPlastic(plastic.name, amount);
        }

        // Data Functions

        public static void save() {
            string json = JsonConvert.SerializeObject(plastics.Values.ToList(), Formatting.Indented);
            File.WriteAllText(saveFile, json);
        }

        public static void load() {
            if (File.Exists(saveFile)) {
                string json = File.ReadAllText(saveFile);
                List<Plastic> plasticsFromFile = JsonConvert.DeserializeObject<List<Plastic>>(json);
                plasticsFromFile.ForEach(addNewPlastic);
            }
        }
    }

    public class Plastic
    {
        public string name;
        public Color colour;
        public int remaining;

        public SolidColorBrush getBrush() {
            if (colour == null) return Brushes.Transparent;
            else return new SolidColorBrush(colour);
        }
    }
}
