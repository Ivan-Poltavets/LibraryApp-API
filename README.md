# Making requests

All queries need to be presented in this form:

https://desolate-cove-47308.herokuapp.com/api/{method_name}
or for the localhost
https://localhost:7256/api/{method_name}

## Methods

### With GET method:

https://.../api/books/freebooks - getting list of free books

https://.../api/books/paidbooks - getting list of paid books

https://.../api/books/{id} - getting book by id

https://.../api/books/{id}/image - getting image by book id

https://.../api/cart/{userId} - getting all items in the cart 

### With POST method:

https://.../api/cart - add book by id to cart

Parameters: bookId(integer), userId(integer)

https://.../api/cart/order - create order from the cart

Parameters: userId(integer)
