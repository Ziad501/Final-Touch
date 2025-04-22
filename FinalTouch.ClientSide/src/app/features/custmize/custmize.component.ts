import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CartService } from '../../core/services/cart.service';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ShopParams } from '../../shared/models/shopparams';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'app-custmize',
  imports: [ReactiveFormsModule, CommonModule, RouterLink,NgSelectModule],
  templateUrl: './custmize.component.html',
  styleUrl: './custmize.component.scss'
})
export class CustmizeComponent implements OnInit {
  productService = inject(ShopService);
  cartService = inject(CartService);
  fb = inject(FormBuilder);
  selectedProduct?: Product;
  shopParams = new ShopParams();
  products?: Product[];

  form = this.fb.group({
    type: ['', Validators.required],
    length: [''],
    width: [''],
    height: [''],
    layers: [''],
    area: [{ value: 0, disabled: true }],
    quantity: [0, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)]],
    pricePerUnit: [{ value: 0, disabled: true }],
    totalPrice: [{ value: 0, disabled: true }]
  });

  ngOnInit(): void {
    this.getProducts();

    this.form.valueChanges.subscribe(values => {
      const type = values.type;
      const length = Number(values.length) || 0;
      const width = Number(values.width) || 0;
      const height = Number(values.height) || 0;
      const layers = Number(values.layers) || 1;

      const product = this.products?.find(p => p.id === +(type ?? 0));
      this.selectedProduct = product;

      const lengthControl = this.form.get('length');
      const widthControl = this.form.get('width');
      const heightControl = this.form.get('height');

      if (product?.type === 'Boots') {
        lengthControl?.clearValidators();
        widthControl?.setValidators([Validators.required, Validators.pattern(/^\d*\.?\d+$/)]);
        heightControl?.setValidators([Validators.required, Validators.pattern(/^\d*\.?\d+$/)]);
      } else {
        lengthControl?.setValidators([Validators.required, Validators.pattern(/^\d*\.?\d+$/)]);
        widthControl?.setValidators([Validators.required, Validators.pattern(/^\d*\.?\d+$/)]);
        heightControl?.clearValidators();
      }

      lengthControl?.updateValueAndValidity({ emitEvent: false });
      widthControl?.updateValueAndValidity({ emitEvent: false });
      heightControl?.updateValueAndValidity({ emitEvent: false });

      const pricePerUnit = product?.price ?? 0;
      let area = length * width;
      let quantity = 0;

      if (product?.type === 'Boards') {
        quantity = Math.ceil((area * layers) / 4);
      } else if (product?.type === 'Boots') {
        quantity = Number(values.quantity) || 0;
      } else {
        quantity = Math.ceil(area / 8);
      }

      const totalPrice = quantity * pricePerUnit;

      this.form.patchValue({
        area,
        quantity,
        pricePerUnit,
        totalPrice
      }, { emitEvent: false });
    });
  }

  getProducts() {
    this.productService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response.data,
      error: error => console.error(error)
    });
  }

  onProductChange(productIdStr: string) {
    const productId = parseInt(productIdStr, 10);
    const product = this.products?.find(p => p.id === productId);
    if (product) {
      this.selectedProduct = product;
      this.form.patchValue({ pricePerUnit: product.price });
    }
  }

  addToCart() {
    if (!this.selectedProduct) return;

    const quantity = this.form.get('quantity')?.value ?? 1;
    this.cartService.addItemToCart(this.selectedProduct, quantity);
    this.clearForm();
  }

  clearForm() {
    const product = this.selectedProduct;
    this.form.reset({
      type: product ? String(product.id) : '',
      length: '',
      width: '',
      height: '',
      layers: '',
      area: 0,
      quantity: 0,
      pricePerUnit: product?.price ?? 0,
      totalPrice: 0
    });
  }

  get isLayerInvalidForBoards(): boolean {
    const layersRaw = this.form.get('layers')?.value;
    const layers = Number(layersRaw);
    return this.selectedProduct?.type === 'Boards' &&
      (!layersRaw || isNaN(layers) || layers <= 0 || !Number.isInteger(layers));
  }
}
