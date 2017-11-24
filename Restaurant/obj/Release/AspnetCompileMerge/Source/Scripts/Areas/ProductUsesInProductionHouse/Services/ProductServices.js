angular.module('myApp').factory('ProductServices', function ($http) {
    return {
        GetAllSellableProduct: function () {

            var sellableproduct = $http.get('/Product/GetSellAbleProductJsonResult').then(function (response) {
                if (response.data.success === true) {
                    return response.data.result;
                    
                } else {
                    toastr.error(reponse.data.errorm);
                }

            }, function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            return sellableproduct;
        },


        GetAllPursableProduct: function (storeId) {
            var purchaseProducts = $http.post('/ProductUsesInProductionHouse/GetPurchaseProductJsonResult', { storeId: storeId }).then(function (response) {
                if (response.data.success === true) {
                    return response.data.result;
                } else {
                    toastr.error(response.data.error);
                }
            }, function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            return purchaseProducts;
        },

        AddRemoveProductByCheckbox: function (selectedProduct, productList) {
            var productIndex = productList.indexOf(selectedProduct);
            if (productIndex === -1) {
                productList.push(selectedProduct);
           
            } else {
                productList.splice(productIndex, 1); //Remove the selected host      
            }
            return productList;
        }
    }
});