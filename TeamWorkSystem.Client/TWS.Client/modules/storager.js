define(function () {

    function get(key) {
        return JSON.parse(localStorage.getItem(key));
    }

    function set(key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    }

    function remove(key) {
        localStorage.removeItem(key);
    }

    function clear() {
        localStorage.clear();
    }

    return {
        get: get,
        set: set,
        remove: remove,
        clear:clear,
    }
});