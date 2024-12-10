const CACHE_NAME = 'fityoufood-cache-v2';
const urlsToCache = [
    '/',
    '/index.html',
    '/static/js/bundle.js',
    '/static/js/main.chunk.js',
    '/static/js/0.chunk.js',
    '/static/css/main.chunk.css',
    '/manifest.json',
    '/favicon.ico',
];

const CACHE_API = [
    '/Exercise',
    '/Training',
];

self.addEventListener('install', (event) => {
    console.log('Service Worker: Installing...');
    event.waitUntil(
        caches.open(CACHE_NAME).then((cache) => {
            console.log('Service Worker: Pre-caching files');
            return Promise.all(
                urlsToCache.map((url) => {
                    return fetch(url)
                        .then((response) => {
                            if (response.ok) {
                                console.log('Service Worker: Caching', url);
                                return cache.put(url, response);
                            } else {
                                console.error('Service Worker: Failed to fetch', url);
                            }
                        })
                        .catch((error) => {
                            console.error('Service Worker: Fetching failed for', url, error);
                        });
                })
            );
        }).catch((error) => {
            console.error('Service Worker: Caching failed', error);
        })
    );
});

self.addEventListener('activate', (event) => {
    console.log('Service Worker: Activating...');
    event.waitUntil(
        caches.keys().then((cacheNames) => {
            return Promise.all(
                cacheNames.map((cacheName) => {
                    if (cacheName !== CACHE_NAME) {
                        console.log(`Service Worker: Removing old cache ${cacheName}`);
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});

self.addEventListener('fetch', (event) => {
    console.log('Service Worker: Fetching', event.request.url);

    if (CACHE_API.some(apiUrl => event.request.url.includes(apiUrl))) {
       
        event.respondWith(
            caches.match(event.request).then((cachedResponse) => {
                if (cachedResponse) {
                    console.log('Service Worker: Returning cached response for', event.request.url);
                    return cachedResponse;
                }

                return fetch(requestWithToken).then((networkResponse) => {
                    if (networkResponse.ok) {
                        return caches.open(CACHE_NAME).then((cache) => {
                            cache.put(requestWithToken, networkResponse.clone());
                            console.log('Service Worker: Cached new response for', event.request.url);
                            return networkResponse;
                        });
                    }
                    return networkResponse; 
                });
            }).catch(() => {
                console.error('Service Worker: Fetch failed for', event.request.url);
                if (event.request.mode === 'navigate') {
                    return caches.match('/index.html');
                }
            })
        );
    } else {
        event.respondWith(
            caches.match(event.request).then((cachedResponse) => {
                return cachedResponse || fetch(event.request);
            })
        );
    }
});

self.addEventListener('message', (event) => {
    if (event.data && event.data.type === 'LOGOUT') {
        console.log('Service Worker: Clearing caches on logout.');
        event.waitUntil(
            caches.keys().then((cacheNames) => {
                return Promise.all(
                    cacheNames.map((cacheName) => caches.delete(cacheName))
                );
            })
        );
    }
});

self.addEventListener('push', (event) => {
    const title = 'FitYouFood Notification';
    const options = {
        body: event.data ? event.data.text() : 'New update available!',
        icon: '/favicon.ico',
    };

    event.waitUntil(self.registration.showNotification(title, options));
});

self.addEventListener('sync', (event) => {
    if (event.tag === 'sync-updates') {
        event.waitUntil(
            fetchUpdatesAndCache()
        );
    }
});

async function fetchUpdatesAndCache() {
    try {
        const response = await fetch('/api/updates');
        const data = await response.json();

        const cache = await caches.open(CACHE_NAME);
        await cache.put('/api/updates', new Response(JSON.stringify(data)));
        console.log('Service Worker: Synced updates successfully.');
    } catch (error) {
        console.error('Service Worker: Failed to sync updates', error);
    }
}
