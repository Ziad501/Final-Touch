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
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink, NgSelectModule],
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
    type: [null, Validators.required],
    length: ['', [Validators.pattern(/^[0-9]*\.?[0-9]+$/)]],
    width: ['', [Validators.pattern(/^[0-9]*\.?[0-9]+$/)]],
    height: ['', [Validators.pattern(/^[0-9]*\.?[0-9]+$/)]],
    layers: ['', [Validators.pattern(/^\d+$/)]],
    color: ['', Validators.required],
    area: [{ value: 0, disabled: true }],
    quantity: [0, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)]],
    pricePerUnit: [{ value: 0, disabled: true }],
    totalPrice: [{ value: 0, disabled: true }]
  });

  ngOnInit(): void {
    const savedForm = sessionStorage.getItem('customizeForm');
    const savedProduct = sessionStorage.getItem('customizeSelectedProduct');

    if (savedForm && savedProduct) {
      this.form.patchValue(JSON.parse(savedForm));
      this.selectedProduct = JSON.parse(savedProduct);
    }

    sessionStorage.removeItem('customizeForm');
    sessionStorage.removeItem('customizeSelectedProduct');

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

      if (this.isBootsLikeType()) {
        lengthControl?.clearValidators();
        widthControl?.setValidators([Validators.required, Validators.pattern(/^[0-9]*\.?[0-9]+$/)]);
        heightControl?.setValidators([Validators.required, Validators.pattern(/^[0-9]*\.?[0-9]+$/)]);
      } else {
        lengthControl?.setValidators([Validators.required, Validators.pattern(/^[0-9]*\.?[0-9]+$/)]);
        widthControl?.setValidators([Validators.required, Validators.pattern(/^[0-9]*\.?[0-9]+$/)]);
        heightControl?.clearValidators();
      }

      lengthControl?.updateValueAndValidity({ emitEvent: false });
      widthControl?.updateValueAndValidity({ emitEvent: false });
      heightControl?.updateValueAndValidity({ emitEvent: false });

      const pricePerUnit = product?.price ?? 0;
      let area = length * width;
      let quantity = 0;

      if (this.isQuantityOnlyType()) {
        quantity = Number(values.quantity) || 0;
      } else if (this.isBootsLikeType() || product?.type === 'ResinArt') {
        quantity = Number(values.quantity) || 0;
      } else if (['GlassCast', 'EastCoastResin', 'GLC'].includes(product?.type ?? '')) {
        quantity = Math.ceil((area * layers) / 4);
      } else {
        quantity = Math.ceil(area / 8);
      }

      const totalPrice = quantity * pricePerUnit;

      const formData = { ...values, area, pricePerUnit, totalPrice };
      sessionStorage.setItem('customizeForm', JSON.stringify(formData));
      if (this.selectedProduct) {
        sessionStorage.setItem('customizeSelectedProduct', JSON.stringify(this.selectedProduct));
      }

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
    this.selectedProduct = undefined;
    sessionStorage.removeItem('customizeForm');
    sessionStorage.removeItem('customizeSelectedProduct');
    this.form.reset({
      type: null,
      length: '',
      width: '',
      height: '',
      layers: '',
      color: '',
      area: 0,
      quantity: 0,
      pricePerUnit: 0,
      totalPrice: 0
    });
  }

  isBootsLikeType(): boolean {
    return this.selectedProduct?.type === 'Interior Door';
  }

  isBoardsLikeType(): boolean {
    return this.selectedProduct?.type === 'Paint';
  }

  isQuantityOnlyType(): boolean {
    return ['Epoxy', 'Paste'].includes(this.selectedProduct?.type || '');
  }

  isAreaBasedType(): boolean {
    return ['Parquet', 'Mosaic', 'Porcelain', 'Indian Porcelain'].includes(this.selectedProduct?.type || '');
  }

  get isLayerInvalidForBoards(): boolean {
    const layersRaw = this.form.get('layers')?.value;
    const layers = Number(layersRaw);
    return this.selectedProduct?.type === 'Boards' &&
      (!layersRaw || isNaN(layers) || layers <= 0 || !Number.isInteger(layers));
  }

  getErrorMessage(controlName: string): string {
    const control = this.form.get(controlName);
    if (!control || !control.errors) return '';
    if (control.hasError('required')) return `${controlName} is required`;
    if (control.hasError('pattern')) return `${controlName} must be a valid number`;
    if (control.hasError('min')) return `${controlName} must be greater than 0`;
    return '';
  }
}
