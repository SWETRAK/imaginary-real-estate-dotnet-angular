import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreOfferBanerComponent } from './ire-offer-baner.component';

describe('IreOfferBanerComponent', () => {
  let component: IreOfferBanerComponent;
  let fixture: ComponentFixture<IreOfferBanerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreOfferBanerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreOfferBanerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
