using DalFacade.DO
namespace Dal;


internal class DataSource
{
 static int readonly=1;
 const int NumOfProduct = 50; // אפשר לעשות מחלקה של קבועים 
 Product[] ProductList = new Product[NumOfProduct];
 static Order[100] order;
 static OrderItem[200] orderitem;
Random rand = new Random(); 
public void CreateProductList()
{
    int[] productId = { 0,1,2,3,4,5,6,7,8,9};
    string[] productNames = { "NecklacesGold","NecklacesSilver","BraceletsGold","BraceletsSilver",
       "EarringsGold","EarringsSilver","RingsGold","RingsSilver","WatchGold","WatchSilver"};
    int[] productInstok = { 0,1 };

    for (int i = 0; i < 10; i++)
    {
        ProductList[i] = new Product();
        // הגרלנו מספר שהוא מיקום במערך השמות
        int number =(int) rand.NextInt64(productNames.Length);
        int id =(int) rand.NextInt64(productId.Length);
        int InStock =(int) rand.NextInt64(productInstok.Length);
        int price =(int) rand.NextInt64(6000,7000);
        ProductList[i].ProductName = productNames[number];
        ProductList[i].ID = productId[id];
        ProductList[i].InStock = productId[InStock];
        ProductList[i].Price = productId[price];
    }
}
    void pushOrder(Order order){

           }
    void pushOrderItem(OrderItem orderItem){
   }
    }
