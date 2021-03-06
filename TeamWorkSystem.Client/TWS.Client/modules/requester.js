﻿define(["jquery", "Q", "modules"], function ($, Q, modules) {
    function makeRequest(url, type, data, content) {
        var deferred = Q.defer();

        var requestOptions = {
            url: url,
            type: type,
            //dataType: "application/x-www-form-urlencoded",
            data: data,
            beforeSend: function (xhr) {
                var token = JSON.parse(localStorage.getItem("token"));

                if (token !== null) {
                    //xhr.withCredentials = true;
                    xhr.setRequestHeader("Authorization", "Bearer " + token);
                }
            },
            success: function resolveDeferred(requestData) {
                deferred.resolve(requestData);
            },
            error: function rejectDeferred(errorData) {
                deferred.reject(JSON.parse(errorData.responseText));
            }
        }

        if (content == null) {
            requestOptions.contentType = "application/json; charset=utf-8";
        }
        else {
            requestOptions.contentType = content;
        }

        $.ajax(requestOptions);

        return deferred.promise;
    }

    function makeGetRequest(url) {
        return makeRequest(url, "get");
    }

    function makePostRequest(url, data, content) {
        return makeRequest(url, "POST", data, content);
    }

    return {
        get: makeGetRequest,
        post: makePostRequest
    }
});