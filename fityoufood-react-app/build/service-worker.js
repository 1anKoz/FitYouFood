// service-worker.js

const CACHE_NAME = 'fityoufood-cache-v1';
const urlsToCache = [
    '/',
    '/index.html',
    '/static/js/bundle.js',
    '/static/js/main.chunk.js',
    '/static/js/0.chunk.js',
    '/static/css/main.chunk.css',
    '/favicon.ico', // Add any other files you need to cache
];

self.addEventListener('install', (event) => {
    console.log('Service Worker: Installing...');
    event.waitUntil(
        caches.open(CACHE_NAME).then((cache) => {
            console.log('Service Worker: Caching files');
            return cache.addAll(urlsToCache);
        })
    );
});

self.addEventListener('activate', (event) => {
    console.log('Service Worker: Activating...');
    const cacheWhitelist = [CACHE_NAME];
    event.waitUntil(
        caches.keys().then((cacheNames) =>
            Promise.all(
                cacheNames.map((cacheName) => {
                    if (!cacheWhitelist.includes(cacheName)) {
                        console.log('Service Worker: Deleting old cache');
                        return caches.delete(cacheName);
                    }
                })
            )
        )
    );
});

self.addEventListener('fetch', (event) => {
    console.log('Service Worker: Fetching', event.request.url);
    event.respondWith(
        caches.match(event.request).then((response) => {
            // Return cached response if available
            return response || fetch(event.request).then((response) => {
                // Cache new requests
                return caches.open(CACHE_NAME).then((cache) => {
                    cache.put(event.request, response.clone());
                    return response;
                });
            });
        })
    );
});
