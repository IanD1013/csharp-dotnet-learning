using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Demo.Generator;

namespace Demo.Benchmarks.Entities;

/*******************************************************/
/* These classes should be picked up by the generator. */
/*******************************************************/
#region Entities

[BogusAttribute]
[ToJsonSerializer]
public partial class Address
{
    public Address(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Address
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Address
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Person
{
    public Person(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Person
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Person
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Order
{
    public Order(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Order
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Order
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class OrderItem
{
    public OrderItem(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class OrderItem
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[ToJsonSerializer]
public partial class OrderItem
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Product
{
    public Product(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Product
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[ToJsonSerializer]
public partial class Product
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class ProductCategory
{
    public ProductCategory(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[ToJsonSerializer]
public partial class ProductCategory
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[ToJsonSerializer]
public partial class ProductCategory
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

/*************************************************************/
/* The following classes should be ignored by the generator. */
/*************************************************************/
#region Entities_00

[BogusAttribute]
[BogusAttribute]
public partial class Address_00
{
    public Address_00(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_00
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_00
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_00
{
    public Person_00(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_00
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_00
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_00
{
    public Order_00(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_00
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_00
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_00
{
    public OrderItem_00(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_00
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_00
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_00
{
    public Product_00(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_00
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_00
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_00
{
    public ProductCategory_00(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_00
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_00
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_01

[BogusAttribute]
[BogusAttribute]
public partial class Address_01
{
    public Address_01(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_01
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_01
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_01
{
    public Person_01(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_01
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_01
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_01
{
    public Order_01(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_01
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_01
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_01
{
    public OrderItem_01(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_01
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_01
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_01
{
    public Product_01(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_01
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_01
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_01
{
    public ProductCategory_01(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_01
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_01
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_02

[BogusAttribute]
[BogusAttribute]
public partial class Address_02
{
    public Address_02(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_02
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_02
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_02
{
    public Person_02(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_02
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_02
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_02
{
    public Order_02(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_02
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_02
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_02
{
    public OrderItem_02(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_02
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_02
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_02
{
    public Product_02(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_02
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_02
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_02
{
    public ProductCategory_02(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_02
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_02
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_03

[BogusAttribute]
[BogusAttribute]
public partial class Address_03
{
    public Address_03(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_03
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_03
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_03
{
    public Person_03(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_03
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_03
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_03
{
    public Order_03(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_03
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_03
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_03
{
    public OrderItem_03(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_03
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_03
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_03
{
    public Product_03(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_03
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_03
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_03
{
    public ProductCategory_03(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_03
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_03
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_04

[BogusAttribute]
[BogusAttribute]
public partial class Address_04
{
    public Address_04(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_04
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_04
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_04
{
    public Person_04(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_04
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_04
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_04
{
    public Order_04(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_04
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_04
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_04
{
    public OrderItem_04(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_04
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_04
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_04
{
    public Product_04(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_04
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_04
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_04
{
    public ProductCategory_04(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_04
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_04
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_05

[BogusAttribute]
[BogusAttribute]
public partial class Address_05
{
    public Address_05(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_05
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_05
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_05
{
    public Person_05(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_05
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_05
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_05
{
    public Order_05(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_05
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_05
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_05
{
    public OrderItem_05(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_05
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_05
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_05
{
    public Product_05(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_05
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_05
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_05
{
    public ProductCategory_05(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_05
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_05
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_06

[BogusAttribute]
[BogusAttribute]
public partial class Address_06
{
    public Address_06(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_06
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_06
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_06
{
    public Person_06(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_06
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_06
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_06
{
    public Order_06(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_06
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_06
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_06
{
    public OrderItem_06(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_06
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_06
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_06
{
    public Product_06(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_06
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_06
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_06
{
    public ProductCategory_06(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_06
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_06
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_07

[BogusAttribute]
[BogusAttribute]
public partial class Address_07
{
    public Address_07(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_07
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_07
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_07
{
    public Person_07(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_07
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_07
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_07
{
    public Order_07(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_07
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_07
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_07
{
    public OrderItem_07(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_07
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_07
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_07
{
    public Product_07(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_07
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_07
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_07
{
    public ProductCategory_07(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_07
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_07
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_08

[BogusAttribute]
[BogusAttribute]
public partial class Address_08
{
    public Address_08(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_08
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_08
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_08
{
    public Person_08(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_08
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_08
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_08
{
    public Order_08(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_08
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_08
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_08
{
    public OrderItem_08(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_08
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_08
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_08
{
    public Product_08(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_08
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_08
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_08
{
    public ProductCategory_08(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_08
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_08
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_09

[BogusAttribute]
[BogusAttribute]
public partial class Address_09
{
    public Address_09(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_09
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_09
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_09
{
    public Person_09(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_09
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_09
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_09
{
    public Order_09(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_09
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_09
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_09
{
    public OrderItem_09(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_09
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_09
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_09
{
    public Product_09(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_09
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_09
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_09
{
    public ProductCategory_09(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_09
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_09
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_10

[BogusAttribute]
[BogusAttribute]
public partial class Address_10
{
    public Address_10(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_10
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_10
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_10
{
    public Person_10(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_10
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_10
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_10
{
    public Order_10(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_10
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_10
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_10
{
    public OrderItem_10(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_10
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_10
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_10
{
    public Product_10(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_10
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_10
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_10
{
    public ProductCategory_10(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_10
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_10
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_11

[BogusAttribute]
[BogusAttribute]
public partial class Address_11
{
    public Address_11(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_11
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_11
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_11
{
    public Person_11(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_11
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_11
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_11
{
    public Order_11(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_11
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_11
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_11
{
    public OrderItem_11(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_11
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_11
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_11
{
    public Product_11(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_11
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_11
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_11
{
    public ProductCategory_11(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_11
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_11
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_12

[BogusAttribute]
[BogusAttribute]
public partial class Address_12
{
    public Address_12(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_12
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_12
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_12
{
    public Person_12(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_12
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_12
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_12
{
    public Order_12(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_12
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_12
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_12
{
    public OrderItem_12(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_12
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_12
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_12
{
    public Product_12(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_12
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_12
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_12
{
    public ProductCategory_12(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_12
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_12
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_13

[BogusAttribute]
[BogusAttribute]
public partial class Address_13
{
    public Address_13(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_13
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_13
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_13
{
    public Person_13(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_13
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_13
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_13
{
    public Order_13(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_13
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_13
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_13
{
    public OrderItem_13(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_13
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_13
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_13
{
    public Product_13(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_13
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_13
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_13
{
    public ProductCategory_13(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_13
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_13
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_14

[BogusAttribute]
[BogusAttribute]
public partial class Address_14
{
    public Address_14(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_14
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_14
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_14
{
    public Person_14(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_14
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_14
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_14
{
    public Order_14(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_14
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_14
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_14
{
    public OrderItem_14(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_14
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_14
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_14
{
    public Product_14(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_14
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_14
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_14
{
    public ProductCategory_14(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_14
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_14
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_15

[BogusAttribute]
[BogusAttribute]
public partial class Address_15
{
    public Address_15(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_15
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_15
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_15
{
    public Person_15(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_15
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_15
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_15
{
    public Order_15(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_15
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_15
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_15
{
    public OrderItem_15(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_15
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_15
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_15
{
    public Product_15(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_15
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_15
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_15
{
    public ProductCategory_15(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_15
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_15
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_16

[BogusAttribute]
[BogusAttribute]
public partial class Address_16
{
    public Address_16(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_16
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_16
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_16
{
    public Person_16(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_16
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_16
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_16
{
    public Order_16(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_16
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_16
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_16
{
    public OrderItem_16(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_16
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_16
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_16
{
    public Product_16(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_16
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_16
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_16
{
    public ProductCategory_16(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_16
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_16
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_17

[BogusAttribute]
[BogusAttribute]
public partial class Address_17
{
    public Address_17(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_17
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_17
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_17
{
    public Person_17(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_17
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_17
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_17
{
    public Order_17(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_17
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_17
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_17
{
    public OrderItem_17(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_17
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_17
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_17
{
    public Product_17(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_17
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_17
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_17
{
    public ProductCategory_17(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_17
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_17
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_18

[BogusAttribute]
[BogusAttribute]
public partial class Address_18
{
    public Address_18(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_18
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_18
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_18
{
    public Person_18(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_18
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_18
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_18
{
    public Order_18(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_18
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_18
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_18
{
    public OrderItem_18(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_18
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_18
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_18
{
    public Product_18(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_18
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_18
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_18
{
    public ProductCategory_18(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_18
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_18
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_19

[BogusAttribute]
[BogusAttribute]
public partial class Address_19
{
    public Address_19(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_19
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_19
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_19
{
    public Person_19(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_19
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_19
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_19
{
    public Order_19(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_19
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_19
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_19
{
    public OrderItem_19(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_19
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_19
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_19
{
    public Product_19(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_19
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_19
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_19
{
    public ProductCategory_19(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_19
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_19
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_20

[BogusAttribute]
[BogusAttribute]
public partial class Address_20
{
    public Address_20(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_20
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_20
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_20
{
    public Person_20(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_20
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_20
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_20
{
    public Order_20(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_20
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_20
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_20
{
    public OrderItem_20(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_20
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_20
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_20
{
    public Product_20(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_20
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_20
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_20
{
    public ProductCategory_20(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_20
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_20
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_21

[BogusAttribute]
[BogusAttribute]
public partial class Address_21
{
    public Address_21(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_21
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_21
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_21
{
    public Person_21(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_21
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_21
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_21
{
    public Order_21(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_21
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_21
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_21
{
    public OrderItem_21(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_21
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_21
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_21
{
    public Product_21(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_21
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_21
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_21
{
    public ProductCategory_21(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_21
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_21
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_22

[BogusAttribute]
[BogusAttribute]
public partial class Address_22
{
    public Address_22(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_22
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_22
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_22
{
    public Person_22(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_22
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_22
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_22
{
    public Order_22(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_22
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_22
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_22
{
    public OrderItem_22(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_22
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_22
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_22
{
    public Product_22(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_22
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_22
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_22
{
    public ProductCategory_22(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_22
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_22
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_23

[BogusAttribute]
[BogusAttribute]
public partial class Address_23
{
    public Address_23(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_23
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_23
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_23
{
    public Person_23(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_23
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_23
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_23
{
    public Order_23(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_23
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_23
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_23
{
    public OrderItem_23(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_23
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_23
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_23
{
    public Product_23(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_23
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_23
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_23
{
    public ProductCategory_23(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_23
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_23
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_24

[BogusAttribute]
[BogusAttribute]
public partial class Address_24
{
    public Address_24(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_24
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_24
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_24
{
    public Person_24(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_24
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_24
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_24
{
    public Order_24(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_24
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_24
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_24
{
    public OrderItem_24(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_24
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_24
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_24
{
    public Product_24(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_24
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_24
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_24
{
    public ProductCategory_24(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_24
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_24
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_25

[BogusAttribute]
[BogusAttribute]
public partial class Address_25
{
    public Address_25(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_25
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_25
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_25
{
    public Person_25(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_25
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_25
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_25
{
    public Order_25(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_25
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_25
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_25
{
    public OrderItem_25(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_25
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_25
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_25
{
    public Product_25(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_25
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_25
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_25
{
    public ProductCategory_25(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_25
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_25
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_26

[BogusAttribute]
[BogusAttribute]
public partial class Address_26
{
    public Address_26(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_26
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_26
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_26
{
    public Person_26(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_26
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_26
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_26
{
    public Order_26(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_26
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_26
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_26
{
    public OrderItem_26(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_26
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_26
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_26
{
    public Product_26(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_26
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_26
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_26
{
    public ProductCategory_26(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_26
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_26
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_27

[BogusAttribute]
[BogusAttribute]
public partial class Address_27
{
    public Address_27(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_27
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_27
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_27
{
    public Person_27(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_27
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_27
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_27
{
    public Order_27(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_27
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_27
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_27
{
    public OrderItem_27(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_27
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_27
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_27
{
    public Product_27(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_27
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_27
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_27
{
    public ProductCategory_27(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_27
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_27
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_28

[BogusAttribute]
[BogusAttribute]
public partial class Address_28
{
    public Address_28(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_28
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_28
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_28
{
    public Person_28(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_28
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_28
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_28
{
    public Order_28(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_28
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_28
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_28
{
    public OrderItem_28(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_28
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_28
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_28
{
    public Product_28(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_28
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_28
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_28
{
    public ProductCategory_28(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_28
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_28
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_29

[BogusAttribute]
[BogusAttribute]
public partial class Address_29
{
    public Address_29(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_29
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_29
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_29
{
    public Person_29(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_29
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_29
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_29
{
    public Order_29(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_29
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_29
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_29
{
    public OrderItem_29(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_29
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_29
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_29
{
    public Product_29(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_29
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_29
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_29
{
    public ProductCategory_29(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_29
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_29
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_30

[BogusAttribute]
[BogusAttribute]
public partial class Address_30
{
    public Address_30(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_30
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_30
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_30
{
    public Person_30(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_30
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_30
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_30
{
    public Order_30(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_30
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_30
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_30
{
    public OrderItem_30(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_30
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_30
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_30
{
    public Product_30(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_30
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_30
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_30
{
    public ProductCategory_30(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_30
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_30
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_31

[BogusAttribute]
[BogusAttribute]
public partial class Address_31
{
    public Address_31(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_31
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_31
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_31
{
    public Person_31(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_31
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_31
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_31
{
    public Order_31(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_31
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_31
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_31
{
    public OrderItem_31(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_31
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_31
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_31
{
    public Product_31(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_31
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_31
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_31
{
    public ProductCategory_31(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_31
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_31
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_32

[BogusAttribute]
[BogusAttribute]
public partial class Address_32
{
    public Address_32(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_32
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_32
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_32
{
    public Person_32(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_32
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_32
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_32
{
    public Order_32(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_32
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_32
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_32
{
    public OrderItem_32(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_32
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_32
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_32
{
    public Product_32(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_32
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_32
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_32
{
    public ProductCategory_32(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_32
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_32
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_33

[BogusAttribute]
[BogusAttribute]
public partial class Address_33
{
    public Address_33(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_33
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_33
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_33
{
    public Person_33(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_33
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_33
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_33
{
    public Order_33(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_33
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_33
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_33
{
    public OrderItem_33(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_33
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_33
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_33
{
    public Product_33(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_33
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_33
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_33
{
    public ProductCategory_33(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_33
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_33
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_34

[BogusAttribute]
[BogusAttribute]
public partial class Address_34
{
    public Address_34(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_34
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_34
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_34
{
    public Person_34(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_34
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_34
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_34
{
    public Order_34(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_34
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_34
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_34
{
    public OrderItem_34(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_34
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_34
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_34
{
    public Product_34(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_34
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_34
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_34
{
    public ProductCategory_34(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_34
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_34
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_35

[BogusAttribute]
[BogusAttribute]
public partial class Address_35
{
    public Address_35(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_35
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_35
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_35
{
    public Person_35(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_35
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_35
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_35
{
    public Order_35(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_35
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_35
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_35
{
    public OrderItem_35(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_35
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_35
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_35
{
    public Product_35(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_35
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_35
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_35
{
    public ProductCategory_35(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_35
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_35
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_36

[BogusAttribute]
[BogusAttribute]
public partial class Address_36
{
    public Address_36(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_36
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_36
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_36
{
    public Person_36(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_36
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_36
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_36
{
    public Order_36(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_36
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_36
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_36
{
    public OrderItem_36(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_36
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_36
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_36
{
    public Product_36(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_36
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_36
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_36
{
    public ProductCategory_36(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_36
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_36
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_37

[BogusAttribute]
[BogusAttribute]
public partial class Address_37
{
    public Address_37(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_37
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_37
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_37
{
    public Person_37(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_37
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_37
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_37
{
    public Order_37(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_37
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_37
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_37
{
    public OrderItem_37(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_37
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_37
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_37
{
    public Product_37(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_37
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_37
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_37
{
    public ProductCategory_37(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_37
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_37
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_38

[BogusAttribute]
[BogusAttribute]
public partial class Address_38
{
    public Address_38(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_38
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_38
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_38
{
    public Person_38(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_38
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_38
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_38
{
    public Order_38(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_38
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_38
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_38
{
    public OrderItem_38(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_38
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_38
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_38
{
    public Product_38(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_38
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_38
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_38
{
    public ProductCategory_38(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_38
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_38
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_39

[BogusAttribute]
[BogusAttribute]
public partial class Address_39
{
    public Address_39(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_39
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_39
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_39
{
    public Person_39(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_39
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_39
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_39
{
    public Order_39(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_39
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_39
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_39
{
    public OrderItem_39(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_39
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_39
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_39
{
    public Product_39(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_39
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_39
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_39
{
    public ProductCategory_39(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_39
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_39
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_40

[BogusAttribute]
[BogusAttribute]
public partial class Address_40
{
    public Address_40(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_40
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_40
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_40
{
    public Person_40(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_40
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_40
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_40
{
    public Order_40(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_40
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_40
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_40
{
    public OrderItem_40(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_40
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_40
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_40
{
    public Product_40(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_40
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_40
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_40
{
    public ProductCategory_40(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_40
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_40
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_41

[BogusAttribute]
[BogusAttribute]
public partial class Address_41
{
    public Address_41(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_41
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_41
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_41
{
    public Person_41(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_41
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_41
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_41
{
    public Order_41(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_41
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_41
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_41
{
    public OrderItem_41(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_41
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_41
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_41
{
    public Product_41(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_41
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_41
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_41
{
    public ProductCategory_41(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_41
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_41
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_42

[BogusAttribute]
[BogusAttribute]
public partial class Address_42
{
    public Address_42(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_42
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_42
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_42
{
    public Person_42(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_42
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_42
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_42
{
    public Order_42(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_42
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_42
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_42
{
    public OrderItem_42(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_42
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_42
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_42
{
    public Product_42(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_42
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_42
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_42
{
    public ProductCategory_42(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_42
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_42
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_43

[BogusAttribute]
[BogusAttribute]
public partial class Address_43
{
    public Address_43(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_43
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_43
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_43
{
    public Person_43(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_43
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_43
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_43
{
    public Order_43(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_43
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_43
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_43
{
    public OrderItem_43(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_43
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_43
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_43
{
    public Product_43(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_43
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_43
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_43
{
    public ProductCategory_43(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_43
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_43
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_44

[BogusAttribute]
[BogusAttribute]
public partial class Address_44
{
    public Address_44(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_44
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_44
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_44
{
    public Person_44(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_44
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_44
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_44
{
    public Order_44(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_44
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_44
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_44
{
    public OrderItem_44(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_44
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_44
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_44
{
    public Product_44(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_44
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_44
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_44
{
    public ProductCategory_44(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_44
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_44
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_45

[BogusAttribute]
[BogusAttribute]
public partial class Address_45
{
    public Address_45(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_45
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_45
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_45
{
    public Person_45(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_45
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_45
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_45
{
    public Order_45(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_45
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_45
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_45
{
    public OrderItem_45(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_45
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_45
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_45
{
    public Product_45(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_45
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_45
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_45
{
    public ProductCategory_45(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_45
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_45
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_46

[BogusAttribute]
[BogusAttribute]
public partial class Address_46
{
    public Address_46(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_46
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_46
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_46
{
    public Person_46(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_46
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_46
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_46
{
    public Order_46(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_46
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_46
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_46
{
    public OrderItem_46(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_46
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_46
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_46
{
    public Product_46(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_46
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_46
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_46
{
    public ProductCategory_46(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_46
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_46
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_47

[BogusAttribute]
[BogusAttribute]
public partial class Address_47
{
    public Address_47(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_47
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_47
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_47
{
    public Person_47(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_47
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_47
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_47
{
    public Order_47(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_47
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_47
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_47
{
    public OrderItem_47(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_47
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_47
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_47
{
    public Product_47(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_47
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_47
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_47
{
    public ProductCategory_47(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_47
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_47
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_48

[BogusAttribute]
[BogusAttribute]
public partial class Address_48
{
    public Address_48(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_48
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_48
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_48
{
    public Person_48(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_48
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_48
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_48
{
    public Order_48(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_48
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_48
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_48
{
    public OrderItem_48(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_48
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_48
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_48
{
    public Product_48(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_48
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_48
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_48
{
    public ProductCategory_48(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_48
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_48
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_49

[BogusAttribute]
[BogusAttribute]
public partial class Address_49
{
    public Address_49(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_49
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_49
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_49
{
    public Person_49(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_49
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_49
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_49
{
    public Order_49(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_49
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_49
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_49
{
    public OrderItem_49(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_49
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_49
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_49
{
    public Product_49(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_49
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_49
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_49
{
    public ProductCategory_49(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_49
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_49
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_50

[BogusAttribute]
[BogusAttribute]
public partial class Address_50
{
    public Address_50(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_50
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_50
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_50
{
    public Person_50(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_50
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_50
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_50
{
    public Order_50(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_50
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_50
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_50
{
    public OrderItem_50(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_50
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_50
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_50
{
    public Product_50(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_50
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_50
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_50
{
    public ProductCategory_50(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_50
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_50
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_51

[BogusAttribute]
[BogusAttribute]
public partial class Address_51
{
    public Address_51(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_51
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_51
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_51
{
    public Person_51(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_51
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_51
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_51
{
    public Order_51(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_51
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_51
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_51
{
    public OrderItem_51(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_51
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_51
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_51
{
    public Product_51(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_51
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_51
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_51
{
    public ProductCategory_51(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_51
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_51
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_52

[BogusAttribute]
[BogusAttribute]
public partial class Address_52
{
    public Address_52(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_52
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_52
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_52
{
    public Person_52(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_52
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_52
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_52
{
    public Order_52(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_52
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_52
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_52
{
    public OrderItem_52(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_52
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_52
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_52
{
    public Product_52(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_52
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_52
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_52
{
    public ProductCategory_52(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_52
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_52
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_53

[BogusAttribute]
[BogusAttribute]
public partial class Address_53
{
    public Address_53(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_53
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_53
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_53
{
    public Person_53(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_53
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_53
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_53
{
    public Order_53(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_53
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_53
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_53
{
    public OrderItem_53(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_53
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_53
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_53
{
    public Product_53(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_53
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_53
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_53
{
    public ProductCategory_53(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_53
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_53
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_54

[BogusAttribute]
[BogusAttribute]
public partial class Address_54
{
    public Address_54(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_54
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_54
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_54
{
    public Person_54(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_54
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_54
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_54
{
    public Order_54(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_54
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_54
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_54
{
    public OrderItem_54(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_54
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_54
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_54
{
    public Product_54(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_54
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_54
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_54
{
    public ProductCategory_54(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_54
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_54
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_55

[BogusAttribute]
[BogusAttribute]
public partial class Address_55
{
    public Address_55(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_55
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_55
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_55
{
    public Person_55(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_55
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_55
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_55
{
    public Order_55(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_55
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_55
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_55
{
    public OrderItem_55(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_55
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_55
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_55
{
    public Product_55(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_55
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_55
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_55
{
    public ProductCategory_55(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_55
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_55
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_56

[BogusAttribute]
[BogusAttribute]
public partial class Address_56
{
    public Address_56(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_56
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_56
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_56
{
    public Person_56(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_56
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_56
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_56
{
    public Order_56(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_56
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_56
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_56
{
    public OrderItem_56(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_56
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_56
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_56
{
    public Product_56(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_56
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_56
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_56
{
    public ProductCategory_56(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_56
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_56
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_57

[BogusAttribute]
[BogusAttribute]
public partial class Address_57
{
    public Address_57(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_57
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_57
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_57
{
    public Person_57(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_57
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_57
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_57
{
    public Order_57(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_57
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_57
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_57
{
    public OrderItem_57(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_57
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_57
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_57
{
    public Product_57(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_57
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_57
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_57
{
    public ProductCategory_57(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_57
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_57
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_58

[BogusAttribute]
[BogusAttribute]
public partial class Address_58
{
    public Address_58(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_58
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_58
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_58
{
    public Person_58(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_58
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_58
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_58
{
    public Order_58(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_58
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_58
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_58
{
    public OrderItem_58(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_58
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_58
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_58
{
    public Product_58(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_58
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_58
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_58
{
    public ProductCategory_58(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_58
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_58
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_59

[BogusAttribute]
[BogusAttribute]
public partial class Address_59
{
    public Address_59(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_59
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_59
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_59
{
    public Person_59(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_59
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_59
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_59
{
    public Order_59(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_59
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_59
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_59
{
    public OrderItem_59(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_59
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_59
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_59
{
    public Product_59(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_59
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_59
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_59
{
    public ProductCategory_59(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_59
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_59
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_60

[BogusAttribute]
[BogusAttribute]
public partial class Address_60
{
    public Address_60(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_60
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_60
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_60
{
    public Person_60(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_60
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_60
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_60
{
    public Order_60(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_60
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_60
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_60
{
    public OrderItem_60(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_60
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_60
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_60
{
    public Product_60(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_60
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_60
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_60
{
    public ProductCategory_60(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_60
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_60
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_61

[BogusAttribute]
[BogusAttribute]
public partial class Address_61
{
    public Address_61(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_61
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_61
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_61
{
    public Person_61(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_61
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_61
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_61
{
    public Order_61(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_61
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_61
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_61
{
    public OrderItem_61(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_61
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_61
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_61
{
    public Product_61(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_61
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_61
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_61
{
    public ProductCategory_61(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_61
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_61
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_62

[BogusAttribute]
[BogusAttribute]
public partial class Address_62
{
    public Address_62(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_62
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_62
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_62
{
    public Person_62(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_62
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_62
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_62
{
    public Order_62(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_62
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_62
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_62
{
    public OrderItem_62(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_62
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_62
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_62
{
    public Product_62(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_62
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_62
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_62
{
    public ProductCategory_62(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_62
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_62
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_63

[BogusAttribute]
[BogusAttribute]
public partial class Address_63
{
    public Address_63(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_63
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_63
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_63
{
    public Person_63(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_63
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_63
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_63
{
    public Order_63(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_63
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_63
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_63
{
    public OrderItem_63(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_63
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_63
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_63
{
    public Product_63(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_63
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_63
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_63
{
    public ProductCategory_63(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_63
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_63
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_64

[BogusAttribute]
[BogusAttribute]
public partial class Address_64
{
    public Address_64(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_64
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_64
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_64
{
    public Person_64(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_64
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_64
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_64
{
    public Order_64(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_64
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_64
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_64
{
    public OrderItem_64(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_64
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_64
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_64
{
    public Product_64(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_64
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_64
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_64
{
    public ProductCategory_64(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_64
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_64
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_65

[BogusAttribute]
[BogusAttribute]
public partial class Address_65
{
    public Address_65(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_65
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_65
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_65
{
    public Person_65(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_65
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_65
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_65
{
    public Order_65(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_65
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_65
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_65
{
    public OrderItem_65(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_65
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_65
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_65
{
    public Product_65(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_65
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_65
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_65
{
    public ProductCategory_65(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_65
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_65
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_66

[BogusAttribute]
[BogusAttribute]
public partial class Address_66
{
    public Address_66(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_66
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_66
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_66
{
    public Person_66(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_66
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_66
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_66
{
    public Order_66(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_66
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_66
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_66
{
    public OrderItem_66(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_66
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_66
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_66
{
    public Product_66(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_66
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_66
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_66
{
    public ProductCategory_66(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_66
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_66
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_67

[BogusAttribute]
[BogusAttribute]
public partial class Address_67
{
    public Address_67(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_67
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_67
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_67
{
    public Person_67(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_67
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_67
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_67
{
    public Order_67(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_67
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_67
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_67
{
    public OrderItem_67(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_67
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_67
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_67
{
    public Product_67(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_67
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_67
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_67
{
    public ProductCategory_67(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_67
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_67
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_68

[BogusAttribute]
[BogusAttribute]
public partial class Address_68
{
    public Address_68(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_68
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_68
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_68
{
    public Person_68(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_68
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_68
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_68
{
    public Order_68(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_68
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_68
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_68
{
    public OrderItem_68(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_68
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_68
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_68
{
    public Product_68(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_68
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_68
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_68
{
    public ProductCategory_68(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_68
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_68
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_69

[BogusAttribute]
[BogusAttribute]
public partial class Address_69
{
    public Address_69(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_69
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_69
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_69
{
    public Person_69(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_69
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_69
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_69
{
    public Order_69(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_69
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_69
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_69
{
    public OrderItem_69(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_69
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_69
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_69
{
    public Product_69(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_69
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_69
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_69
{
    public ProductCategory_69(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_69
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_69
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_70

[BogusAttribute]
[BogusAttribute]
public partial class Address_70
{
    public Address_70(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_70
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_70
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_70
{
    public Person_70(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_70
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_70
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_70
{
    public Order_70(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_70
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_70
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_70
{
    public OrderItem_70(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_70
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_70
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_70
{
    public Product_70(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_70
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_70
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_70
{
    public ProductCategory_70(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_70
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_70
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_71

[BogusAttribute]
[BogusAttribute]
public partial class Address_71
{
    public Address_71(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_71
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_71
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_71
{
    public Person_71(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_71
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_71
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_71
{
    public Order_71(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_71
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_71
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_71
{
    public OrderItem_71(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_71
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_71
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_71
{
    public Product_71(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_71
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_71
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_71
{
    public ProductCategory_71(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_71
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_71
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_72

[BogusAttribute]
[BogusAttribute]
public partial class Address_72
{
    public Address_72(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_72
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_72
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_72
{
    public Person_72(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_72
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_72
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_72
{
    public Order_72(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_72
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_72
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_72
{
    public OrderItem_72(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_72
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_72
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_72
{
    public Product_72(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_72
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_72
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_72
{
    public ProductCategory_72(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_72
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_72
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_73

[BogusAttribute]
[BogusAttribute]
public partial class Address_73
{
    public Address_73(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_73
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_73
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_73
{
    public Person_73(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_73
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_73
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_73
{
    public Order_73(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_73
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_73
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_73
{
    public OrderItem_73(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_73
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_73
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_73
{
    public Product_73(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_73
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_73
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_73
{
    public ProductCategory_73(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_73
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_73
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_74

[BogusAttribute]
[BogusAttribute]
public partial class Address_74
{
    public Address_74(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_74
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_74
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_74
{
    public Person_74(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_74
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_74
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_74
{
    public Order_74(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_74
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_74
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_74
{
    public OrderItem_74(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_74
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_74
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_74
{
    public Product_74(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_74
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_74
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_74
{
    public ProductCategory_74(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_74
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_74
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_75

[BogusAttribute]
[BogusAttribute]
public partial class Address_75
{
    public Address_75(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_75
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_75
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_75
{
    public Person_75(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_75
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_75
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_75
{
    public Order_75(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_75
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_75
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_75
{
    public OrderItem_75(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_75
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_75
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_75
{
    public Product_75(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_75
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_75
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_75
{
    public ProductCategory_75(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_75
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_75
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_76

[BogusAttribute]
[BogusAttribute]
public partial class Address_76
{
    public Address_76(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_76
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_76
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_76
{
    public Person_76(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_76
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_76
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_76
{
    public Order_76(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_76
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_76
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_76
{
    public OrderItem_76(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_76
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_76
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_76
{
    public Product_76(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_76
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_76
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_76
{
    public ProductCategory_76(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_76
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_76
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_77

[BogusAttribute]
[BogusAttribute]
public partial class Address_77
{
    public Address_77(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_77
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_77
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_77
{
    public Person_77(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_77
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_77
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_77
{
    public Order_77(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_77
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_77
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_77
{
    public OrderItem_77(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_77
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_77
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_77
{
    public Product_77(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_77
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_77
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_77
{
    public ProductCategory_77(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_77
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_77
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_78

[BogusAttribute]
[BogusAttribute]
public partial class Address_78
{
    public Address_78(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_78
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_78
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_78
{
    public Person_78(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_78
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_78
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_78
{
    public Order_78(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_78
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_78
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_78
{
    public OrderItem_78(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_78
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_78
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_78
{
    public Product_78(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_78
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_78
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_78
{
    public ProductCategory_78(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_78
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_78
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_79

[BogusAttribute]
[BogusAttribute]
public partial class Address_79
{
    public Address_79(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_79
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_79
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_79
{
    public Person_79(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_79
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_79
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_79
{
    public Order_79(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_79
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_79
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_79
{
    public OrderItem_79(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_79
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_79
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_79
{
    public Product_79(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_79
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_79
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_79
{
    public ProductCategory_79(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_79
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_79
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_80

[BogusAttribute]
[BogusAttribute]
public partial class Address_80
{
    public Address_80(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_80
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_80
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_80
{
    public Person_80(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_80
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_80
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_80
{
    public Order_80(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_80
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_80
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_80
{
    public OrderItem_80(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_80
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_80
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_80
{
    public Product_80(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_80
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_80
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_80
{
    public ProductCategory_80(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_80
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_80
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_81

[BogusAttribute]
[BogusAttribute]
public partial class Address_81
{
    public Address_81(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_81
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_81
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_81
{
    public Person_81(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_81
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_81
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_81
{
    public Order_81(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_81
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_81
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_81
{
    public OrderItem_81(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_81
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_81
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_81
{
    public Product_81(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_81
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_81
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_81
{
    public ProductCategory_81(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_81
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_81
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_82

[BogusAttribute]
[BogusAttribute]
public partial class Address_82
{
    public Address_82(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_82
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_82
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_82
{
    public Person_82(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_82
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_82
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_82
{
    public Order_82(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_82
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_82
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_82
{
    public OrderItem_82(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_82
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_82
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_82
{
    public Product_82(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_82
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_82
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_82
{
    public ProductCategory_82(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_82
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_82
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_83

[BogusAttribute]
[BogusAttribute]
public partial class Address_83
{
    public Address_83(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_83
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_83
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_83
{
    public Person_83(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_83
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_83
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_83
{
    public Order_83(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_83
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_83
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_83
{
    public OrderItem_83(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_83
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_83
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_83
{
    public Product_83(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_83
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_83
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_83
{
    public ProductCategory_83(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_83
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_83
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_84

[BogusAttribute]
[BogusAttribute]
public partial class Address_84
{
    public Address_84(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_84
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_84
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_84
{
    public Person_84(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_84
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_84
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_84
{
    public Order_84(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_84
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_84
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_84
{
    public OrderItem_84(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_84
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_84
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_84
{
    public Product_84(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_84
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_84
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_84
{
    public ProductCategory_84(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_84
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_84
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_85

[BogusAttribute]
[BogusAttribute]
public partial class Address_85
{
    public Address_85(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_85
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_85
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_85
{
    public Person_85(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_85
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_85
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_85
{
    public Order_85(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_85
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_85
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_85
{
    public OrderItem_85(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_85
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_85
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_85
{
    public Product_85(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_85
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_85
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_85
{
    public ProductCategory_85(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_85
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_85
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_86

[BogusAttribute]
[BogusAttribute]
public partial class Address_86
{
    public Address_86(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_86
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_86
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_86
{
    public Person_86(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_86
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_86
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_86
{
    public Order_86(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_86
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_86
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_86
{
    public OrderItem_86(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_86
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_86
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_86
{
    public Product_86(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_86
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_86
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_86
{
    public ProductCategory_86(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_86
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_86
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_87

[BogusAttribute]
[BogusAttribute]
public partial class Address_87
{
    public Address_87(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_87
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_87
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_87
{
    public Person_87(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_87
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_87
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_87
{
    public Order_87(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_87
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_87
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_87
{
    public OrderItem_87(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_87
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_87
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_87
{
    public Product_87(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_87
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_87
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_87
{
    public ProductCategory_87(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_87
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_87
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_88

[BogusAttribute]
[BogusAttribute]
public partial class Address_88
{
    public Address_88(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_88
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_88
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_88
{
    public Person_88(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_88
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_88
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_88
{
    public Order_88(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_88
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_88
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_88
{
    public OrderItem_88(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_88
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_88
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_88
{
    public Product_88(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_88
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_88
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_88
{
    public ProductCategory_88(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_88
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_88
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_89

[BogusAttribute]
[BogusAttribute]
public partial class Address_89
{
    public Address_89(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_89
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_89
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_89
{
    public Person_89(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_89
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_89
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_89
{
    public Order_89(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_89
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_89
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_89
{
    public OrderItem_89(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_89
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_89
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_89
{
    public Product_89(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_89
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_89
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_89
{
    public ProductCategory_89(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_89
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_89
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_90

[BogusAttribute]
[BogusAttribute]
public partial class Address_90
{
    public Address_90(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_90
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_90
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_90
{
    public Person_90(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_90
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_90
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_90
{
    public Order_90(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_90
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_90
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_90
{
    public OrderItem_90(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_90
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_90
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_90
{
    public Product_90(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_90
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_90
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_90
{
    public ProductCategory_90(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_90
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_90
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_91

[BogusAttribute]
[BogusAttribute]
public partial class Address_91
{
    public Address_91(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_91
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_91
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_91
{
    public Person_91(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_91
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_91
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_91
{
    public Order_91(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_91
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_91
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_91
{
    public OrderItem_91(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_91
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_91
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_91
{
    public Product_91(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_91
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_91
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_91
{
    public ProductCategory_91(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_91
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_91
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_92

[BogusAttribute]
[BogusAttribute]
public partial class Address_92
{
    public Address_92(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_92
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_92
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_92
{
    public Person_92(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_92
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_92
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_92
{
    public Order_92(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_92
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_92
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_92
{
    public OrderItem_92(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_92
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_92
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_92
{
    public Product_92(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_92
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_92
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_92
{
    public ProductCategory_92(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_92
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_92
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_93

[BogusAttribute]
[BogusAttribute]
public partial class Address_93
{
    public Address_93(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_93
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_93
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_93
{
    public Person_93(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_93
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_93
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_93
{
    public Order_93(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_93
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_93
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_93
{
    public OrderItem_93(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_93
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_93
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_93
{
    public Product_93(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_93
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_93
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_93
{
    public ProductCategory_93(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_93
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_93
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_94

[BogusAttribute]
[BogusAttribute]
public partial class Address_94
{
    public Address_94(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_94
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_94
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_94
{
    public Person_94(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_94
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_94
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_94
{
    public Order_94(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_94
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_94
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_94
{
    public OrderItem_94(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_94
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_94
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_94
{
    public Product_94(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_94
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_94
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_94
{
    public ProductCategory_94(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_94
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_94
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_95

[BogusAttribute]
[BogusAttribute]
public partial class Address_95
{
    public Address_95(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_95
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_95
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_95
{
    public Person_95(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_95
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_95
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_95
{
    public Order_95(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_95
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_95
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_95
{
    public OrderItem_95(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_95
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_95
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_95
{
    public Product_95(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_95
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_95
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_95
{
    public ProductCategory_95(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_95
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_95
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_96

[BogusAttribute]
[BogusAttribute]
public partial class Address_96
{
    public Address_96(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_96
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_96
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_96
{
    public Person_96(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_96
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_96
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_96
{
    public Order_96(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_96
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_96
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_96
{
    public OrderItem_96(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_96
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_96
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_96
{
    public Product_96(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_96
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_96
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_96
{
    public ProductCategory_96(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_96
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_96
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_97

[BogusAttribute]
[BogusAttribute]
public partial class Address_97
{
    public Address_97(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_97
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_97
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_97
{
    public Person_97(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_97
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_97
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_97
{
    public Order_97(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_97
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_97
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_97
{
    public OrderItem_97(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_97
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_97
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_97
{
    public Product_97(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_97
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_97
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_97
{
    public ProductCategory_97(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_97
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_97
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_98

[BogusAttribute]
[BogusAttribute]
public partial class Address_98
{
    public Address_98(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_98
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_98
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_98
{
    public Person_98(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_98
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_98
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_98
{
    public Order_98(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_98
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_98
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_98
{
    public OrderItem_98(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_98
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_98
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_98
{
    public Product_98(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_98
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_98
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_98
{
    public ProductCategory_98(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_98
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_98
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion

#region Entities_99

[BogusAttribute]
[BogusAttribute]
public partial class Address_99
{
    public Address_99(string addressLine1, string? addressLine2, string city, string state, string postalCode)
    {
        _addressLine1 = addressLine1;
        _addressLine2 = addressLine2;
        _city = city;
        _state = state;
        _postalCode = postalCode;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_99
{
    private readonly string _addressLine1;
    private readonly string? _addressLine2;
    private readonly string _city;
    private readonly string _state;
    private readonly string _postalCode;
}

[BogusAttribute]
[BogusAttribute]
public partial class Address_99
{
    public required string AddressLine1
    {
        get => _addressLine1;
        [MemberNotNull(nameof(_addressLine1))]
        init => _addressLine1 = value;
    }

    public required string? AddressLine2
    {
        get => _addressLine2;
        init => _addressLine2 = value;
    }
    
    public required string City
    {
        get => _city;
        [MemberNotNull(nameof(_city))]
        init => _city = value;
    }
    
    public required string State
    {
        get => _state;
        [MemberNotNull(nameof(_state))]
        init => _state = value;
    }

    public required string PostalCode
    {
        get => _postalCode;
        [MemberNotNull(nameof(_postalCode))]
        init => _postalCode = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_99
{
    public Person_99(string firstName, string lastName, string? middleName, string emailAddress, string phoneNumber, Guid billingAddressId, Guid shippingAddressId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _middleName = middleName;
        _emailAddress = emailAddress;
        _phoneNumber = phoneNumber;
        _billingAddressId = billingAddressId;
        _shippingAddressId = shippingAddressId;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_99
{
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly string? _middleName;
    private readonly string _emailAddress;
    private readonly string _phoneNumber;
    private readonly Guid _billingAddressId;
    private readonly Guid _shippingAddressId;
}

[BogusAttribute]
[BogusAttribute]
public partial class Person_99
{
    public required string FirstName
    {
        get => _firstName;
        [MemberNotNull(nameof(_firstName))]
        init => _firstName = value;
    }

    public required string LastName
    {
        get => _lastName;
        [MemberNotNull(nameof(_lastName))]
        init => _lastName = value;
    }

    public required string? MiddleName
    {
        get => _middleName;
        init => _middleName = value;
    }    
    public required string EmailAddress
    {
        get => _emailAddress;
        [MemberNotNull(nameof(_emailAddress))]
        init => _emailAddress = value;
    }

    public required string PhoneNumber
    {
        get => _phoneNumber;
        [MemberNotNull(nameof(_phoneNumber))]
        init => _phoneNumber = value;
    }
    
    public required Guid BillingAddressId
    {
        get => _billingAddressId;
        init => _billingAddressId = value;
    }

    public required Guid ShippingAddressId
    {
        get => _shippingAddressId;
        init => _shippingAddressId = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_99
{
    public Order_99(Guid id, Guid customerId, string orderNumber, string orderDate, string orderStatus, string orderStatusDescription)
    {
        _id = id;
        _customerId = customerId;
        _orderNumber = orderNumber;
        _orderDate = orderDate;
        _orderStatus = orderStatus;
        _orderStatusDescription = orderStatusDescription;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_99
{
    private readonly Guid _id;
    private readonly Guid _customerId;
    private readonly string _orderNumber;
    private readonly string _orderDate;
    private readonly string _orderStatus;
    private readonly string _orderStatusDescription;
}

[BogusAttribute]
[BogusAttribute]
public partial class Order_99
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CustomerId
    {
        get => _customerId;
        init => _customerId = value;
    }

    public required string OrderNumber
    {
        get => _orderNumber;
        [MemberNotNull(nameof(_orderNumber))]
        init => _orderNumber = value;
    }
    
    public required string OrderDate
    {
        get => _orderDate;
        [MemberNotNull(nameof(_orderDate))]
        init => _orderDate = value;
    }

    public required string OrderStatus
    {
        get => _orderStatus;
        [MemberNotNull(nameof(_orderStatus))]
        init => _orderStatus = value;
    }

    public required string OrderStatusDescription
    {
        get => _orderStatusDescription;
        [MemberNotNull(nameof(_orderStatusDescription))]
        init => _orderStatusDescription = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_99
{
    public OrderItem_99(Guid id, Guid orderId, Guid productId, decimal unitPrice, int quantity)
    {
        _id = id;
        _orderId = orderId;
        _productId = productId;
        _unitPrice = unitPrice;
        _quantity = quantity;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_99
{
    private readonly Guid _id;
    private readonly Guid _orderId;
    private readonly Guid _productId;
    private readonly decimal _unitPrice;
    private readonly int _quantity;
}

[BogusAttribute]
[BogusAttribute]
public partial class OrderItem_99
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid OrderId
    {
        get => _orderId;
        init => _orderId = value;
    }

    public required Guid ProductId
    {
        get => _productId;
        init => _productId = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }

    public required int Quantity
    {
        get => _quantity;
        init => _quantity = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_99
{
    public Product_99(Guid id, Guid categoryId, string sku, string upc, string name, string description, string imageUrl, decimal unitPrice)
    {
        _id = id;
        _categoryId = categoryId;
        _sku = sku;
        _upc = upc;
        _name = name;
        _description = description;
        _imageUrl = imageUrl;
        _unitPrice = unitPrice;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_99
{
    private readonly Guid _id;
    private readonly Guid _categoryId;
    private readonly string _sku;
    private readonly string _upc;
    private readonly string _name;
    private readonly string _description;
    private readonly string _imageUrl;
    private readonly decimal _unitPrice;
}

[BogusAttribute]
[BogusAttribute]
public partial class Product_99
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required Guid CategoryId
    {
        get => _categoryId;
        init => _categoryId = value;
    }

    public required string SKU
    {
        get => _sku;
        [MemberNotNull(nameof(_sku))]
        init => _sku = value;
    }

    public required string UPC
    {
        get => _upc;
        [MemberNotNull(nameof(_upc))]
        init => _upc = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }

    public required string ImageUrl
    {
        get => _imageUrl;
        [MemberNotNull(nameof(_imageUrl))]
        init => _imageUrl = value;
    }

    public required decimal UnitPrice
    {
        get => _unitPrice;
        init => _unitPrice = value;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_99
{
    public ProductCategory_99(Guid id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
    }
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_99
{
    private readonly Guid _id;
    private readonly string _name;
    private readonly string _description;
}

[BogusAttribute]
[BogusAttribute]
public partial class ProductCategory_99
{
    public required Guid Id
    {
        get => _id;
        init => _id = value;
    }

    public required string Name
    {
        get => _name;
        [MemberNotNull(nameof(_name))]
        init => _name = value;
    }

    public required string Description
    {
        get => _description;
        [MemberNotNull(nameof(_description))]
        init => _description = value;
    }
}

#endregion