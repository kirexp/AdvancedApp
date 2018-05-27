using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;
namespace App1.Extenssions
{
    public class LocationProvider
    {
        public async Task<Position> GetGps()
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                {
                    return null;
                }
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null);
                return position;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Address>> GetAddressByPosition(Position position)
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                {
                    return null;
                }
                var locator = CrossGeolocator.Current;
                var address = await locator.GetAddressesForPositionAsync(position, "RJHqIE53Onrqons5CNOx~FrDr3XhjDTyEXEjng-CRoA~Aj69MhNManYUKxo6QcwZ0wmXBtyva0zwuHB04rFYAPf7qqGJ5cHb03RCDw1jIW8l");
                return address;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }
    }
}
