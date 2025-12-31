// Geolocation helper for Blazor
window.getGeolocation = () => {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject("Geolocation not supported");
            return;
        }
        
        navigator.geolocation.getCurrentPosition(
            (pos) => resolve({ 
                latitude: pos.coords.latitude, 
                longitude: pos.coords.longitude 
            }),
            (err) => reject(err.message),
            { 
                enableHighAccuracy: true, 
                timeout: 10000, 
                maximumAge: 60000 
            }
        );
    });
};

// Check permission state without triggering prompt
window.checkLocationPermission = async () => {
    if (!navigator.permissions) {
        return 'unknown';
    }
    try {
        const status = await navigator.permissions.query({ name: 'geolocation' });
        return status.state; // 'granted', 'denied', or 'prompt'
    } catch {
        return 'unknown';
    }
};
