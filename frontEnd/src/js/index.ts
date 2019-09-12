import axios, { AxiosResponse, AxiosError } from "../../node_modules/axios/index"
import {Recipe} from "./Recipe"
import {weight} from "./weight"

// Html elementerne
let divElement : HTMLDivElement = <HTMLDivElement> document.getElementById("content");
let idagBtn:HTMLButtonElement = <HTMLButtonElement> document.getElementById("idagBtn");
let ugeBtn: HTMLButtonElement = <HTMLButtonElement>document.getElementById("ugeBtn");
let årBtn: HTMLButtonElement = <HTMLButtonElement>document.getElementById("årBtn");
let månedsGennemsnitBtn = <HTMLButtonElement>document.getElementById("månedsGennemsnitBtn");
let Månedvalg = <HTMLButtonElement>document.getElementById("Månedvalg");
let notifikationDiv = <HTMLDivElement>document.getElementById("notifikation");
let ugensMaxBtn = <HTMLButtonElement>document.getElementById("størsteUge");
let ugensMinBtn = <HTMLButtonElement>document.getElementById("mindsteUge");
let getRecipeBtn = <HTMLButtonElement>document.getElementById("getRecipeBtn");
let searchRecipe = <HTMLInputElement>document.getElementById("searchRecipe");
let madOpskriftHistoBtn = <HTMLButtonElement>document.getElementById("madOpskriftBtn");

//knappernes event listeners
getRecipeBtn.addEventListener('click', getRecipe);
idagBtn.addEventListener('click', plotIdag);
ugeBtn.addEventListener('click', plotUge);
årBtn.addEventListener('click', plotÅr);
månedsGennemsnitBtn.addEventListener('click', månedsGennemsnit);
ugensMaxBtn.addEventListener("click",ugensMax);
ugensMinBtn.addEventListener("click",ugensMin);
madOpskriftHistoBtn.addEventListener("click",getAllRecipes);

//funktioner der skal køres ved execution
console.log("Hej")
setTimeout(madSpildFaldet,1000);

//Api id og key
let Api_id = "766e4b4b"
let ApiKey = "7e6955147a2399b858406e2993bbdbd2";

//variable til at indholde brugers opskrift søg ord. 
let ApiKeyword = "";

//variable der indholder den aktuelle tid og dato
let dateTimeNow = new Date().toLocaleString();


function getAllRecipes():void{
    
    axios.get<Recipe[]>("https://restsmarttrashservice.azurewebsites.net/api/recipe")
    .then(function(response: AxiosResponse<Recipe[]>): void
    {
        console.log(response);

        let result: string = "<ul>"
        
        response.data.forEach((recipe: Recipe) => {
            result += "<li>"+"Dato:"+" "+recipe.dato+"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Opskrift: "+" "+recipe.recipe+"</li>"    
        });
        result +="</ul>"

        divElement.innerHTML = result;
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllRecipes function");

}

//funktion til at poste en opskrift til databasen
function postRecipe(dato1:string, recipe1:string):void{   

    axios.post<Recipe>("https://restsmarttrashservice.azurewebsites.net/api/recipe",
                    {dato:dato1, recipe: recipe1})
        .then(function (response:AxiosResponse):void{
            console.log("status koden er:" + response.status)    
        })
        .catch(function(error: AxiosError):void{

        })
}

//funktion til at hente en opskrift fra tredjeparts API'en
function getRecipe():void{
    ApiKeyword = searchRecipe.value;
    axios.get<object>("https://api.edamam.com/search?q="+ApiKeyword+"&app_id="+Api_id+"&app_key="+ApiKey+"&from=0&to=1")
    .then(function(response: AxiosResponse<object>): void
    {
        console.log(response);
        //konvertere responsen til string ved hjælp af JSON.stringify
        let voresData:string = JSON.stringify(response.data);
        //det første og andet ord hvor response string skal splittes
        let firstWord = "\"ingredientLines";
        let secondWord = "\"ingredients\"";
        //index på respektiv splitte orde
        let index1 = voresData.indexOf(firstWord);
        let index2 = voresData.indexOf(secondWord);
        
        let newData = voresData.substring(index1,index2);

        divElement.innerHTML = newData;
        //poster opskriften straks til databasen
        postRecipe(dateTimeNow,newData);
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getRecipe function");
}

function plotIdag():void{

    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/1")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);

        let result: string = "<ul>"
        
        response.data.forEach((weight: weight) => {
            result += "<li>"+"Dato:"+" "+weight.weightMeasure+"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Vægt: "+" "+weight.dato+ " g" + "</li>"    
        });
        result +="</ul>"

        divElement.innerHTML = result;
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllCustomers function");
}

function plotUge():void{

    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/2")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);

        let result: string = "<ul>"
        
        response.data.forEach((weight: weight) => {
            result += "<li>"+"Dato: "+weight.weightMeasure+"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Vægt: "+weight.dato+ " g" + "</li>"    
        });
        result +="</ul>"

        divElement.innerHTML = result;
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllCustomers function");
}

function sortUgenEfterStørrelse(w : weight[]):weight[]{

    let sortedList:weight[] = new Array;
    /* sortere listen af weight objektor efter weight attributtet*/
    sortedList = w.sort((n1,n2) => Number(n1.dato) - Number(n2.dato));

    return sortedList;
}

function ugensMax():void{
    
    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/2")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);
        
        let myList:weight[] = new Array;
        myList =  response.data;
        let sortedList = sortUgenEfterStørrelse(myList).reverse();
        let størsteDag = sortedList[0];
        

        divElement.innerHTML = "Dag: " + størsteDag.weightMeasure.substring(0,10) +"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Vægt: "+ størsteDag.dato+" g";
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllCustomers function");
}

function ugensMin():void{
    
    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/2")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);
        
        let myList:weight[] = new Array;
        myList =  response.data;
        let sortedList = sortUgenEfterStørrelse(myList);
        let mindsteDag = sortedList[0];
        

        divElement.innerHTML = "Dag: " + mindsteDag.weightMeasure.substring(0,10) +"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Vægt: "+ mindsteDag.dato+" g";
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllCustomers function");
}

function månedsGennemsnit():void{

    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/3")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);
        console.log("r i then");
        let result: number[] = new Array;

        response.data.forEach((weight: weight) => {
            let gram = Number(weight.dato);
            result.push(gram) 
        });
        /*gå igennem result listen og lægger tallene sammen */
       let sum = result.reduce((a, b) => a + b, 0)
       
       let myÅrMåned = (response.data[0].weightMeasure).substring(0,7);
        
        divElement.innerHTML = "Måned: "+myÅrMåned+"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Gennemsnits Madspild: "+String((sum/result.length).toFixed(2)+" g");
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllCustomers function");
}

function plotÅr():void{

    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/4")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);

        let result: string = "<ul>"
        
        response.data.forEach((weight: weight) => {
            result += "<li>"+"Dato:"+" "+weight.weightMeasure+"&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;"+"Vægt: "+weight.dato+ " g" + "</li>"  
        });
        result +="</ul>"

        divElement.innerHTML = result;
    })
    .catch(
        function(error: AxiosError ): void{
            console.log("errrrrrror in my code")
            console.log(error);
        }
        
    )   
    console.log("er i slutning af getAllCustomers function");
}

function madSpildFaldet():void{
    let nuværendeUge: string | number
    let forrigeUge
    axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/2")
    .then(function(response: AxiosResponse<weight[]>): void
    {
        console.log(response);
        
        let result: number[] = new Array;

        response.data.forEach((weight: weight) => {
            let gram = Number(weight.dato);
            result.push(gram) 
        });
            /*gå igennem result listen og lægger tallene sammen */
            nuværendeUge = result.reduce((a, b) => a + b, 0)
            axios.get<weight[]>("https://restsmarttrashservice.azurewebsites.net/api/weight/5")
            .then(function(response: AxiosResponse<weight[]>): void
            {
                console.log(response);
                
                let result: number[] = new Array;
        
                response.data.forEach((weight: weight) => {
                    let gram = Number(weight.dato);
                    result.push(gram) 
                });
                    /* gå igennem result listen og lægger tallene sammen */
                    forrigeUge = result.reduce((a, b) => a + b, 0) 
                    if(nuværendeUge<forrigeUge){
                    notifikationDiv.innerHTML = "NOTIFIKATION: &nbsp;&nbsp;" + "Dit madspild er faldet: &nbsp;&nbsp;" + "Nuværende Uge: "+nuværendeUge+ " g"+" &nbsp;- &nbsp;" + "Forrige Uge: "+forrigeUge+ " g";
                    }
                }) 

    })
   

   

    console.log("er i slutning af getAllCustomers function");
}

