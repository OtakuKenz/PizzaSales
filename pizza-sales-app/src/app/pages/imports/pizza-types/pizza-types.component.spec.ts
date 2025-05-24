import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PizzaTypesComponent } from './pizza-types.component';

describe('PizzaTypesComponent', () => {
  let component: PizzaTypesComponent;
  let fixture: ComponentFixture<PizzaTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PizzaTypesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PizzaTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
