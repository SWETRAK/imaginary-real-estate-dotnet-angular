import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IreOfferAuthorBanerComponent } from './ire-offer-author-baner.component';

describe('IreOfferAuthorBanerComponent', () => {
  let component: IreOfferAuthorBanerComponent;
  let fixture: ComponentFixture<IreOfferAuthorBanerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IreOfferAuthorBanerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IreOfferAuthorBanerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
