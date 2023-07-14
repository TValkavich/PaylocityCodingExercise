export default function checkForDiscountEmployee(employeeName){
    if(employeeName.substring(0,1).toLowerCase() === 'a'){
        return 1;
    }

    return 0;
}