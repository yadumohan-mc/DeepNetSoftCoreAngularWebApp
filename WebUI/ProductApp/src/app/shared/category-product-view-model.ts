import { Category } from "./category";
import { Product } from "./product";

export class CategoryProductViewModel {
    Category!:Category;
    SubCategories!:Category[];
    Products!:Product[];
}
