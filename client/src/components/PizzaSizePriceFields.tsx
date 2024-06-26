import { Button } from "components/ui/Button.tsx";
import Input from "components/ui/Input.tsx";
import Label from "components/ui/Label.tsx";
import { PizzaCreateSchemaType } from "interfaces/zod/pizza.ts";
import { UseFormRegister } from "react-hook-form";

import React from "react";

interface PizzaSizePriceFieldsProps {
  index: number;
  register: UseFormRegister<PizzaCreateSchemaType>;
  removeSize: (index: number) => void;
}

const PizzaSizePriceFields: React.FC<PizzaSizePriceFieldsProps> = ({ index, register, removeSize }) => {
  return (
    <div className="flex items-center space-x-2">
      <div>
        <Label htmlFor={`sizes[${index}].sizeId`}>Size ID</Label>
        <Input {...register(`sizes.${index}.sizeId`)} type="number" id={`sizes[${index}].sizeId`} />
      </div>
      <div>
        <Label htmlFor={`sizes[${index}].price`}>Price</Label>
        <Input {...register(`sizes.${index}.price`)} type="number" id={`sizes[${index}].price`} />
      </div>
      <Button type="button" variant="danger" size="sm" onClick={() => removeSize(index)}>
        Remove
      </Button>
    </div>
  );
};

export default PizzaSizePriceFields;
