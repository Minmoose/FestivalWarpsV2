//This is free and unencumbered software released into the public domain.

//Anyone is free to copy, modify, publish, use, compile, sell, or
//distribute this software, either in source code form or as a compiled
//binary, for any purpose, commercial or non-commercial, and by any
//means.

//In jurisdictions that recognize copyright laws, the author or authors
//of this software dedicate any and all copyright interest in the
//software to the public domain.We make this dedication for the benefit
//of the public at large and to the detriment of our heirs and
//successors. We intend this dedication to be an overt act of
//relinquishment in perpetuity of all present and future rights to this
//software under copyright law.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
//OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//OTHER DEALINGS IN THE SOFTWARE.

//For more information, please refer to <https://unlicense.org>

using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace FestivalWarpsV2
{
    public class ModEntry : Mod
    {
        // We should avoid using the 'new' keyword in loops, as it causes strain on the garbage collector, an increase in memory usage and can cause hitching in the game 
        // So we should declare variables outside of the update loop 

        string currentSeason;
        string currentLocationName;

        bool doIntersect = true;

        Rectangle virilityFestival_BoundingBox = new Rectangle(3776, 960, 64, 192);
        Rectangle fuckFest_BoundingBox = new Rectangle(2432, 0, 128, 64);
        Rectangle phallicCelebration_BoundingBox = new Rectangle(0, 3392, 64, 192);

        Rectangle seedShop_BoundingBox = new Rectangle(384, 1792, 64, 64);
        Rectangle communityCenter_BoundingBox = new Rectangle(2048, 1472, 64, 64);

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
            {
                // We should "fail" fast and return as there is not a save loaded yet 
                return;
            }

            // Make sure the player's currentLocation is not null
            if (Game1.hasLoadedGame && Game1.player.currentLocation != null)
            {
                // Get the name of the player's current location
                currentLocationName = Game1.player.currentLocation?.Name;

                // Check to see if there is a festival active and if so make sure the current location is named Temp
                if (Game1.isFestival() && currentLocationName.ToLowerInvariant() == "temp")
                {
                    // Get the current season now as we potentially might have to call ToLowerInvariant() multiple times otherwise 
                    currentSeason = Game1.currentSeason.ToLowerInvariant();

                    //
                    // Virility Festival, Spring 19, Woods
                    //

                    // Check to make sure we're on the correct day and season 
                    if (Game1.dayOfMonth == 19 && Game1.whereIsTodaysFest == "Woods" && currentSeason == "spring")
                    {
                        // Get the players bounding box and check to see if it intersects with the exit's bounding box, 
                        if (Game1.player.GetBoundingBox().Intersects(virilityFestival_BoundingBox))
                        {
                            // Do intersect is set to false when the player clicks cancel on the dialog that pops up 
                            if (doIntersect)
                            {
                                // It's allright that there is a "new" keyword here, as the effort required to remove it is too high and would not be worth to fix 
                                Game1.activeClickableMenu = new ConfirmationDialog(Game1.content.LoadString("Strings\\StringsFromCSFiles:Event.cs.1758", "Virility Festival"), Game1.CurrentEvent.forceEndFestival, closeDialog);
                            }
                        }
                        // The dialogue will not reopen until the player leaves the intersected bounding box and enters it again 
                        else if (doIntersect == false)
                        {
                            doIntersect = true;
                        }
                    }

                    //
                    // Fuck Fest, Summer 20, Beach
                    //

                    else if (Game1.dayOfMonth == 20 && Game1.whereIsTodaysFest == "Beach" && currentSeason == "summer")
                    {
                        if (Game1.player.GetBoundingBox().Intersects(fuckFest_BoundingBox))
                        {
                            if (doIntersect)
                            {
                                Game1.activeClickableMenu = new ConfirmationDialog(Game1.content.LoadString("Strings\\StringsFromCSFiles:Event.cs.1758", "Fuck Fest"), Game1.CurrentEvent.forceEndFestival, closeDialog);
                            }
                        }
                        else if (doIntersect == false)
                        {
                            doIntersect = true;
                        }
                    }

                    //
                    // Phallic Celebration, Fall 12, Town
                    //

                    else if (Game1.dayOfMonth == 12 && Game1.whereIsTodaysFest == "Town" && currentSeason == "fall")
                    {
                        if (Game1.player.GetBoundingBox().Intersects(phallicCelebration_BoundingBox))
                        {
                            if (doIntersect)
                            {
                                Game1.activeClickableMenu = new ConfirmationDialog(Game1.content.LoadString("Strings\\StringsFromCSFiles:Event.cs.1758", "Phallic Celebration"), Game1.CurrentEvent.forceEndFestival, closeDialog);
                            }
                        }
                        else if (doIntersect == false)
                        {
                            doIntersect = true;
                        }
                    }

                    //
                    // Day of Carnality, Winter 13, Seed Shop & Community Center
                    //

                    else if (Game1.dayOfMonth == 13 && currentSeason == "winter")
                    {
                        // Here we separate the Seed Shop and Community Center for cleaner looking code, 

                        // Seed Shop
                        if (Game1.whereIsTodaysFest == "SeedShop")
                        {
                            if (Game1.player.GetBoundingBox().Intersects(seedShop_BoundingBox))
                            {
                                if (doIntersect)
                                {
                                    Game1.activeClickableMenu = new ConfirmationDialog(Game1.content.LoadString("Strings\\StringsFromCSFiles:Event.cs.1758", "Day of Carnality"), Game1.CurrentEvent.forceEndFestival, closeDialog);
                                }
                            }
                            else if (doIntersect == false)
                            {
                                doIntersect = true;
                            }
                        }

                        // Community Center
                        if (Game1.whereIsTodaysFest == "CommunityCenter")
                        {
                            if (Game1.player.GetBoundingBox().Intersects(communityCenter_BoundingBox))
                            {
                                if (doIntersect)
                                {
                                    Game1.activeClickableMenu = new ConfirmationDialog(Game1.content.LoadString("Strings\\StringsFromCSFiles:Event.cs.1758", "Day of Carnality"), Game1.CurrentEvent.forceEndFestival, closeDialog);
                                }
                            }
                            else if (doIntersect == false)
                            {
                                doIntersect = true;
                            }
                        }

                    }
                }
            }
        }

        public virtual void closeDialog(Farmer who)
        {
            // Make sure dialogue does not get stuck open by setting doIntersect to false 
            doIntersect = false;

            Game1.exitActiveMenu();
        }

    }
}