export default function checkForDiscountEmployee(dependents){
    let counter = 0;

    dependents.forEach((item) => {
        if(item.name.substring(0,1).toLowerCase() === 'a'){
            counter++;
        }
    })

    return counter; 
}