import type { IProduct } from "../../app/models/product";
import ProductList from "./ProductList";

type Props = {
  products: IProduct[];
};

export default function Catalog({ products }: Props) {
  return (
    <>
      <ProductList products={products} />
    </>
  );
}
